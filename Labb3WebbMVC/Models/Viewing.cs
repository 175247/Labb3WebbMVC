﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3WebbMVC.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int MovieId { get; set; }
    }
}
