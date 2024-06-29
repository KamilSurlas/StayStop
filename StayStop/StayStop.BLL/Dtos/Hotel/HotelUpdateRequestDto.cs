
namespace StayStop.BLL.Dtos.Hotel
{
    public class HotelUpdateRequestDto
    {
        public HotelType? HotelType { get; set; }
        public int? Stars { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public List<string>? Images { get; set; }
    }
}
