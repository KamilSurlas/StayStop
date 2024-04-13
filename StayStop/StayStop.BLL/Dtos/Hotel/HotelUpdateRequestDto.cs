
namespace StayStop.BLL.Dtos.Hotel
{
    public class HotelUpdateRequestDto
    {
        public List<StayStop.Model.User>? Managers { get; set; }
        public List<StayStop.Model.Room>? Rooms { get; set; }
        public HotelType? HotelType { get; set; }
        public int? Stars { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Descritpion { get; set; }
        public string? CoverImage { get; set; }
        public List<string>? Images { get; set; }
    }
}
