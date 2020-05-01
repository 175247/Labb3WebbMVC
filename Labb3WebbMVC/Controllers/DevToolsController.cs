using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb3WebbMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb3WebbMVC.Controllers
{
    public class DevToolsController : Controller
    {
        private readonly CinemaContext _context;

        public DevToolsController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<RedirectResult> SeedDb()
        {
            var movieFromDb = await _context.MovieList.FirstOrDefaultAsync(m => m.Rating == "5/7");

            if (movieFromDb == null)
            {
                await _context.MovieList.AddAsync(new Movie
                {
                    Title = "Pontus: Bouncer of Shangri-La",
                    Duration = "13h 37min",
                    Rating = "5/7",
                    TrailerURL = "https://www.youtube.com/embed/KAOdjqyG37A",
                    Synopsis = "Wayward prince and heir to the Bhutan throne, is crowned King Pontus I after an alien attack robs the earth of all lions. " +
                    "Now the young king must navigate palace politics, the lack of roars left behind, and the emotional strings of his past life as a russian hacker. " +
                    "Watch it now, blyat! Rawr...",
                    Viewing = new List<Viewing>
                    {
                        new Viewing
                        {
                            Salon = new Salon
                            {
                                Number = 2,
                                SeatCapacity = 100,
                                RemainingSeats = 2
                            },
                            StartTime = new DateTime(2020, 04, 23, 15, 45, 00)
                        },
                        new Viewing
                        {
                            Salon = new Salon
                            {
                                Number = 2,
                                SeatCapacity = 100,
                                RemainingSeats = 0
                            },
                            StartTime = new DateTime(2020, 04, 23, 20, 30, 00)
                        },
                        new Viewing
                        {
                            Salon = new Salon
                            {
                                Number = 1,
                                SeatCapacity = 50,
                                RemainingSeats = 4
                            },
                            StartTime = new DateTime(2020, 04, 27, 10, 15, 00)
                        }
                    }
                });

                await _context.MovieList.AddAsync(new Movie
                {
                    Title = "The Matrix",
                    Duration = "2h 16min",
                    Rating = "8.7/10",
                    TrailerURL = "https://www.youtube.com/embed/D4eJx-0g3Do",
                    Synopsis = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    Viewing = new List<Viewing>
                    {
                        new Viewing
                        {
                            Salon = new Salon
                            {
                                Number = 1,
                                SeatCapacity = 50,
                                RemainingSeats = 44
                            },
                            StartTime = new DateTime(2020, 04, 23, 15, 45, 00)
                        },
                        new Viewing
                        {
                            Salon = new Salon
                            {
                                Number = 1,
                                SeatCapacity = 50,
                                RemainingSeats = 17
                            },
                            StartTime = new DateTime(2020, 04, 25, 20, 30, 00)
                        },
                        new Viewing
                        {
                            Salon = new Salon
                            {
                                Number = 2,
                                SeatCapacity = 100,
                                RemainingSeats = 91
                            },
                            StartTime = new DateTime(2020, 04, 27, 10, 15, 00)
                        }
                    }
                });
                await _context.SaveChangesAsync();
                return Redirect("../");
            }
            else
            {
                return Redirect("~/");
            }
        }

        public async Task<RedirectResult> EmptyDb()
        {
            foreach (var viewing in _context.Viewing)
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

            return Redirect("~/");
        }
        public RedirectResult RecreateDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            return Redirect("~/");
        }
    }
}