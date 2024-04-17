using System.ComponentModel.DataAnnotations;

namespace StayStop.BLL.Dtos.Hotel
{
    public class HotelRequestDto
    {
        public required HotelType HotelType { get; set; }
        public required int Stars { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string ZipCode { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CoverImage { get; set; }
        public required List<string> Images { get; set; }
    }
}
