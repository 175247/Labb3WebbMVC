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
