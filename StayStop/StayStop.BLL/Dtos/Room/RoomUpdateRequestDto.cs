
namespace StayStop.BLL.Dtos.Room
{
    public class RoomUpdateRequestDto
    {
        public string? Description { get; set; }
        public RoomType? RoomType { get; set; }
        public int? NumberOfChildren { get; set; }
        public int? NumberOfAdults { get; set; }    
        public bool? IsAvailable { get; set; }
        public decimal? PriceForAdult { get; set; }
        public decimal? PriceForChild { get; set; }
        public string? CoverImage { get; set; }
        public IEnumerable<string>? Images { get; set; }
    }
}
