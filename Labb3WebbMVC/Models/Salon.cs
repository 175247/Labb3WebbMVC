using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3WebbMVC.Models
{
    public class Salon
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int RemainingSeats { get; set; }
    }
}
