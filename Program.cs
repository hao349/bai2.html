using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourBookingApp.Models
{
    public class Tour
    {
        public int Id { get; set; }

        [Required, StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(1, 30)]
        public int Duration { get; set; }

        public string Description { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourBookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        public string CustomerName { get; set; }

        [Required, EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        public int TourId { get; set; }

        [ForeignKey("TourId")]
        public Tour Tour { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int NumberOfPeople { get; set; }
    }
}
