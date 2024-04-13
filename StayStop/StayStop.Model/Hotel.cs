using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.Model
{
    public class Hotel 
    {
        [Key]
        public int HotelId { get; set; }
        public List<User> Managers { get; } = [];
        public required List<Room> Rooms { get; set; }
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
