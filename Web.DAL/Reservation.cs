using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.DAL
{
   public class Reservation
    {
        [Key]
        public int id { get; set; }
        public string GuestNumber { get; set; }
        public string ReservationDate { get; set; }
        public string ManuType { get; set; }
        public string Notes { get; set; }
    }
}
