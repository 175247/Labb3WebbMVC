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

        public MoviesController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieList.ToListAsync());
        }

        public async Task<IActionResult> DisplayMovieInfo(int id)
        {
            var movieFromDb = await _context.MovieList.Where(m => m.Id == id).ToListAsync();
            var selectedSalon = await _context.SalonList.FirstOrDefaultAsync(s => s.Id == id);
            var selectedViews = await _context.Viewing.Where(v => v.MovieId == id).ToListAsync();
            movieFromDb[0].Salon.Number = selectedSalon.Number;
            movieFromDb[0].Viewing = selectedViews;

            if (movieFromDb[0].Title.Contains("Pontus"))
            {
                return View("DisplayPontus", movieFromDb);
            }
            else
            {
                return View(movieFromDb);
            }
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

        public async Task<IActionResult> SeedDb()
        {
            var movieFromDb = await _context.MovieList.FirstOrDefaultAsync(m => m.Rating == "5/7");

            if (movieFromDb == null)
            {
                await _context.MovieList.AddAsync(new Movie
                {
                    Title = "Pontus: Bouncer of Shangri-La",
                    Duration = "13h 37min",
                    Rating = "5/7",
                    Salon = new Salon
                    {
                        Number = 1,
                        SeatCapacity = 50,
                        RemainingSeats = 0
                    },
                    Viewing = new List<Viewing>
                    {
                        new Viewing { StartTime = new DateTime(2020, 04, 23, 15, 45, 00) },
                        new Viewing { StartTime = new DateTime(2020, 04, 23, 20, 30, 00) },
                        new Viewing { StartTime = new DateTime(2020, 04, 27, 10, 15, 00) }
                    }
                });

                await _context.MovieList.AddAsync(new Movie
                {
                    Title = "The Matrix",
                    Duration = "2h 16min",
                    Rating = "8.7/10",
                    Salon = new Salon
                    {
                        Number = 2,
                        SeatCapacity = 100,
                        RemainingSeats = 44
                    },
                    Viewing = new List<Viewing>
                    {
                        new Viewing { StartTime = new DateTime(2020, 04, 23, 15, 45, 00) },
                        new Viewing { StartTime = new DateTime(2020, 04, 25, 20, 30, 00) },
                        new Viewing { StartTime = new DateTime(2020, 04, 27, 10, 15, 00) }
                    }
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EmptyDb()
        {
            foreach(var viewing in _context.Viewing)
            {
                _context.Viewing.Remove(viewing);
            }
            foreach (var movie in _context.MovieList)
            {
                _context.MovieList.Remove(movie);
            }
            foreach (var salon in _context.SalonList)
            {
                _context.SalonList.Remove(salon);
            }
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}
