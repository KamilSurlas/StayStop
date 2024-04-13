

namespace StayStop.BLL.Dtos.Room
{
    public class RoomResponseDto
    {
        public required string Description { get; set; }
        public required RoomType RoomType { get; set; }
        public required string CoverImage { get; set; }
        public required string Images { get; set; }
        public required bool IsAvailable { get; set; }
        public required decimal PriceForAdult { get; set; }
        public required decimal PriceForChild { get; set; }
    }
}
