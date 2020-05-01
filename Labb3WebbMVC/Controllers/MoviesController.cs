using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb3WebbMVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Labb3WebbMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaContext _context;

        public MoviesController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            _context.Database.Migrate();
            return View(await _context.MovieList.ToListAsync());
        }

        public async Task<List<Movie>> GetSpecificMovie(int id)
        {
            var movie = await _context.MovieList
                .Where(m => m.Id == id)
                .Include(v => v.Viewing)
                .ThenInclude(s => s.Salon)
                .ToListAsync();

            return movie;
        }

        public async Task<IActionResult> Sorting(int id, string order)
        {
            var movie = await GetSpecificMovie(id);

            if (movie.Count == 0)
            {
                return View("Index");
            }

            if (order == "times")
            {
                movie[0].Viewing = movie[0].Viewing.OrderByDescending(v => v.StartTime).ToList();
            }
            else if (order == "seats")
            {
                movie[0].Viewing = movie[0].Viewing.OrderByDescending(s => s.Salon.RemainingSeats).ToList();
            }

            return View("DisplayMovieInfo", movie[0]);
        }

        public async Task<IActionResult> DisplayMovieInfo(int id)
        {
            var selectedMovie = await GetSpecificMovie(id);

            if (selectedMovie.Count == 0)
            {
                return View("Index");
            }

            HttpContext.Session.SetString("SessionMovie", JsonConvert.SerializeObject(selectedMovie));

            return View(selectedMovie[0]);
        }

        public IActionResult BookTicketView(int id)
        {
            var movieDeserialized = JsonConvert.DeserializeObject<List<Movie>>(
                HttpContext.Session.GetString("SessionMovie"));

            var viewingToCast = movieDeserialized[0].Viewing.Where(v => v.Id == id).ToList();

            viewingToCast[0].MovieTitle = movieDeserialized[0].Title;
            var viewing = (Viewing)viewingToCast[0];

            return View(viewing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizeBooking(int id, [Bind("Id,StartTime,MovieId,MovieTitle, SalonId, Salon")] Viewing receivedModel)
        {
            if (id != receivedModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var salonFromDb = await _context.SalonList.Where(s => s.Id == receivedModel.SalonId).ToListAsync();
                var salon = (Salon)salonFromDb[0];

                if (salon.RemainingSeats <= 0)
                {
                    return View("BookTicketView", receivedModel);
                }
                
                salon.RemainingSeats -= receivedModel.Salon.RemainingSeats;
                receivedModel.Salon = salon;

                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(receivedModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("BookingConfirmation", receivedModel);
            }
            return View("BookTicketView", receivedModel);
        }

        public async Task<IActionResult> BookingConfirmation(Viewing viewing)
        {
            var salon = await _context.SalonList.Where(s => s.Id == viewing.SalonId).ToListAsync();
            viewing.Salon = salon[0];
            var receipt = viewing;

            return View(receipt);
        }

        private bool MovieExists(int id)
        {
            return _context.MovieList.Any(e => e.Id == id);
        }
    }
}
