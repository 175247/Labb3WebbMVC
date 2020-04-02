using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb3WebbMVC.Models;

namespace Labb3WebbMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaContext _context;
        private List<Movie> selectedMovie;

        public MoviesController(CinemaContext context)
        {
            _context = context;
            selectedMovie = new List<Movie>();
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            _context.Database.Migrate();
            return View(await _context.MovieList.ToListAsync());
        }

        public async Task<IActionResult> DisplayMovieInfo(int id)
        {
            selectedMovie = await _context.MovieList.Where(m => m.Id == id).ToListAsync();
            var selectedViews = await _context.Viewing.Where(v => v.MovieId == id).ToListAsync();
            var salonList = await _context.SalonList.ToListAsync();

            foreach (var item in selectedViews)
            {
                item.Salon = salonList.Find(s => s.Id == item.SalonId);
            }
            
            selectedMovie[0].Viewing = selectedViews;
            if (selectedMovie[0].Title.Contains("Pontus"))
            {
                return View("DisplayPontus", selectedMovie);
            }
            else
            {
                return View(selectedMovie);
            }
        }

        public IActionResult BookTicketView(Viewing viewing)
        {
            var selectedViewing = selectedMovie[0].Viewing.Where(v => v.Id == viewing.Id).ToList();
            return View(selectedViewing);
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
