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
        public required List<StayStop.Model.User> Managers { get; set; }
        public required List<StayStop.Model.Room> Rooms { get; set; }
        public required HotelType HotelType { get; set; }
        public required int Stars { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string ZipCode { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Name { get; set; }
        public required string Descritpion { get; set; }
        public required string CoverImage { get; set; }
        public required List<string> Images { get; set; }
    }
}
