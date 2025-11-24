namespace WEB_API.Models
{
    
    public class Room
    {
        
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int DormitoryNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}