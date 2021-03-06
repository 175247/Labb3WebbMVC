﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3WebbMVC.Models
{
    public class Salon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        public int SeatCapacity { get; set; }
        [Required]
        public int RemainingSeats { get; set; }
    }
}
