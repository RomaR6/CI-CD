using Microsoft.AspNetCore.Mvc;

using WEB_API.Models;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAllRooms([FromQuery] int? dormitoryNumber, [FromQuery] bool? isAvailable)
        {
            IEnumerable<Room> rooms = InMemoryDb.Rooms;

            
            if (dormitoryNumber.HasValue)
            {
                rooms = rooms.Where(r => r.DormitoryNumber == dormitoryNumber.Value);
            }

            
            if (isAvailable.HasValue)
            {
                rooms = rooms.Where(r => r.IsAvailable == isAvailable.Value);
            }

            return Ok(rooms.ToList());
        }

        
        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(int id)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // POST: api/rooms
        [HttpPost]
        public ActionResult<Room> CreateRoom(Room room)
        {
            

            room.Id = InMemoryDb.Rooms.Any() ? InMemoryDb.Rooms.Max(r => r.Id) + 1 : 1;
            InMemoryDb.Rooms.Add(room);

            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, Room updatedRoom)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            

            room.RoomNumber = updatedRoom.RoomNumber;
            room.DormitoryNumber = updatedRoom.DormitoryNumber;
            room.Capacity = updatedRoom.Capacity;
            
            room.IsAvailable = updatedRoom.IsAvailable;

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            if (InMemoryDb.Bookings.Any(b => b.RoomId == id))
            {
                return BadRequest("Неможливо видалити кімнату, оскільки вона заброньована.");
            }

            InMemoryDb.Rooms.Remove(room);
            return NoContent();
        }
    }
}