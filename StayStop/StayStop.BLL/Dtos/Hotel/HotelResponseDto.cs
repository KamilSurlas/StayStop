using StayStop.BLL.Dtos.Room;
using StayStop.BLL.Dtos.User;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.Hotel
{
    public class HotelResponseDto
    {
        public required int HotelId { get; set; }
        public required List<RoomResponseDto> Rooms { get; set; }
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
