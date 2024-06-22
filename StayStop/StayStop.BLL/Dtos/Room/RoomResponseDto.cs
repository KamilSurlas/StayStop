

namespace StayStop.BLL.Dtos.Room
{
    public class RoomResponseDto
    {
        public required int RoomId { get; set; }
        public required string Description { get; set; }
        public required RoomType RoomType { get; set; }
        public required string CoverImage { get; set; }
        public required List<string> Images { get; set; }
        public required int NumberOfChildren { get; set; }
        public required int NumberOfAdults { get; set; }
        public required bool IsAvailable { get; set; }
        public required decimal PriceForAdult { get; set; }
        public required decimal PriceForChild { get; set; }
    }
}
