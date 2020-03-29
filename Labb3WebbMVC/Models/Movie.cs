using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3WebbMVC.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public DateTime StartingTime { get; set; }
        public Salon Salon { get; set; }
    }
}
