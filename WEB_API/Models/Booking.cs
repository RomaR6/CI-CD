using System.ComponentModel.DataAnnotations;

namespace WEB_API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "StudentId є обов'язковим.")]
        [Range(1, int.MaxValue, ErrorMessage = "StudentId має бути додатним числом.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "RoomId є обов'язковим.")]
        [Range(1, int.MaxValue, ErrorMessage = "RoomId має бути додатним числом.")]
        public int RoomId { get; set; }

        public DateTime BookingDate { get; set; }
    }
}