using Microsoft.AspNetCore.Mvc;

using WEB_API.Models;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAllBookings([FromQuery] int? studentId, [FromQuery] int? roomId)
        {
            IEnumerable<Booking> bookings = InMemoryDb.Bookings;

            if (studentId.HasValue)
            {
                bookings = bookings.Where(b => b.StudentId == studentId.Value);
            }

            if (roomId.HasValue)
            {
                bookings = bookings.Where(b => b.RoomId == roomId.Value);
            }

            return Ok(bookings.ToList());
        }

        
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(int id)
        {
            var booking = InMemoryDb.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        
        [HttpPost]
        public ActionResult<Booking> CreateBooking(Booking booking)
        {
            
            var student = InMemoryDb.Students.FirstOrDefault(s => s.Id == booking.StudentId);
            if (student == null)
            {
                return BadRequest("Студента з таким ID не знайдено.");
            }

            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            if (room == null)
            {
                return BadRequest("Кімнату з таким ID не знайдено.");
            }
            if (!room.IsAvailable)
            {
                return BadRequest("Ця кімната вже заброньована.");
            }
            if (InMemoryDb.Bookings.Any(b => b.StudentId == booking.StudentId))
            {
                return BadRequest("Цей студент вже має активне бронювання.");
            }

            room.IsAvailable = false; // Бронюємо
            booking.Id = InMemoryDb.Bookings.Any() ? InMemoryDb.Bookings.Max(b => b.Id) + 1 : 1;
            booking.BookingDate = DateTime.UtcNow;
            InMemoryDb.Bookings.Add(booking);

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = InMemoryDb.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            if (room != null)
            {
                room.IsAvailable = true;
            }

            InMemoryDb.Bookings.Remove(booking);
            return NoContent();
        }
    }
}