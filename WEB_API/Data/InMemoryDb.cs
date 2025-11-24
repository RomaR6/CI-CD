
using WEB_API.Models;

namespace WEB_API.Models
{
    public static class InMemoryDb
    {
        public static List<Student> Students { get; set; } = new List<Student>
        {
            new Student { Id = 1, FirstName = "Іван", LastName = "Петренко", Email = "ivan@example.com", Course = 2 },
            new Student { Id = 2, FirstName = "Марія", LastName = "Іванова", Email = "maria@example.com", Course = 3 },
            new Student { Id = 3, FirstName = "Олексій", LastName = "Сидоренко", Email = "oleksiy@example.com", Course = 1 },
            new Student { Id = 4, FirstName = "Тетяна", LastName = "Мельник", Email = "tetiana@example.com", Course = 4 },
            new Student { Id = 5, FirstName = "Андрій", LastName = "Коваль", Email = "andriy@example.com", Course = 2 }
        };

        public static List<Room> Rooms { get; set; } = new List<Room>
        {
            new Room { Id = 1, RoomNumber = 101, DormitoryNumber = 5, Capacity = 2, IsAvailable = true },
            new Room { Id = 2, RoomNumber = 102, DormitoryNumber = 5, Capacity = 3, IsAvailable = false }, 
            new Room { Id = 3, RoomNumber = 205, DormitoryNumber = 6, Capacity = 2, IsAvailable = true },
            new Room { Id = 4, RoomNumber = 206, DormitoryNumber = 6, Capacity = 2, IsAvailable = false }, 
            new Room { Id = 5, RoomNumber = 310, DormitoryNumber = 5, Capacity = 1, IsAvailable = true },
            new Room { Id = 6, RoomNumber = 401, DormitoryNumber = 7, Capacity = 4, IsAvailable = false }, 
            new Room { Id = 7, RoomNumber = 402, DormitoryNumber = 7, Capacity = 4, IsAvailable = true }
        };

        public static List<Booking> Bookings { get; set; } = new List<Booking>
        {
            new Booking { Id = 1, StudentId = 2, RoomId = 2, BookingDate = DateTime.UtcNow.AddDays(-10) },
            new Booking { Id = 2, StudentId = 3, RoomId = 4, BookingDate = DateTime.UtcNow.AddDays(-5) },
            new Booking { Id = 3, StudentId = 4, RoomId = 6, BookingDate = DateTime.UtcNow.AddDays(-20) }
        };
    }
}