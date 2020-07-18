using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP.Models
{
    public class Reservation
    {
        [Required]
        public string GuestNumber { get; set; }
        public string ReservationDate { get; set; }
        public string ManuType { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "The value cannot exceed 300 characters. ")]
        public string Notes { get; set; }
    }
}

