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
        public DbSet<Viewing> Viewing { get; set; }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
            
        }
    }
}

/*
 * TODO:
 * Move Salon to the viewing-model.
  That way, every viewing will have an option for a separate salon
  Every viewing will then have separate seats as well.

 * Once the above is implemented, start working on a booking page for a specific viewing.
*/