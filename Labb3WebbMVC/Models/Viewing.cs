﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3WebbMVC.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int SalonId { get; set; }
        [Required]
        public Salon Salon { get; set; }
    }
}
