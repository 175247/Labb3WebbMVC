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

        [BindProperty]
        public Viewing Viewing { get; set; }

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

        public async Task<IActionResult> Sorting(int id, string order)
        {
            var movie = await _context.MovieList.Where(m => m.Id == id).Include(v => v.Viewing).ThenInclude(s => s.Salon).ToListAsync();
            
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
            var selectedMovie = await _context.MovieList
                .Where(m => m.Id == id)
                .Include(v => v.Viewing)
                .ThenInclude(s => s.Salon)
                .ToListAsync();

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

            //HttpContext.Session.SetString("SessionViewing", JsonConvert.SerializeObject(viewing));

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
                else
                {
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
            }
            return View("BookTicketView", receivedModel);
        }

        public async Task<IActionResult> BookingConfirmation(Viewing viewing)
        {
            var salon = await _context.SalonList.Where(s => s.Id == viewing.SalonId).ToListAsync();
            viewing.Salon = salon[0];
            //var viewing = await _context.Viewing.Where(v => v.Id == id).Include(s => s.Salon).ToListAsync();
            //var movieTitle = await _context.MovieList.FirstOrDefaultAsync(v => v.Id == viewing[0].MovieId);
            var receipt = viewing;
            return View(receipt);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.MovieList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Duration,StartingTime")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.MovieList.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Duration,StartingTime")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.MovieList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.MovieList.FindAsync(id);
            _context.MovieList.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.MovieList.Any(e => e.Id == id);
        }
    }
}
