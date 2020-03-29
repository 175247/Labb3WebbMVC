using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3WebbMVC.Models
{
    public class CinemaContext : DbContext
    {
        public DbSet<Movie> MovieList { get; set; }
        public DbSet<Salon> SalonList { get; set; }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
            
        }

        //public void SeedDatabase()
        //{
        //    this.MovieList.AddAsync(new Movie
        //    {
        //        Title = "Pontus: Bouncer of Shangri-La",
        //        Duration = "13h 37min",
        //        Rating = "5/7",
        //        Salon = new Salon
        //        { 
        //            Number = 1, 
        //            RemainingSeats = 0 
        //        },
        //        StartingTime = new DateTime(2020, 04, 23, 20, 30, 00)
        //    });
        //
        //    this.MovieList.AddAsync(new Movie
        //    {
        //        Title = "The Matrix",
        //        Duration = "2h 16min",
        //        Rating = "8.7/10",
        //        Salon = new Salon
        //        {
        //            Number = 2,
        //            RemainingSeats = 44
        //        },
        //        StartingTime = new DateTime(2020, 04, 22, 20, 30, 00)
        //    });
        //
        //    this.SaveChangesAsync();
        //}
    }
}
