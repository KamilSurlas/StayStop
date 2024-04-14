using AutoMapper;
using StayStop.BLL.Dtos;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Opinion;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.Dtos.Room;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL_EF.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Hotel
            CreateMap<HotelRequestDto, Hotel>().ReverseMap();
            CreateMap<HotelUpdateRequestDto, Hotel>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // TEST UPDATE


            // Room
            CreateMap<RoomRequestDto, Room>().ReverseMap();
            CreateMap<RoomUpdateRequestDto, Room>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            // Opinion
            CreateMap<RoomRequestDto, Room>();
            CreateMap<Opinion, OpinionResponseDto>()
                  .ForMember(dest => dest.Hotel, opt => opt.MapFrom((src, dest,_, context) =>
                      context.Mapper.Map<HotelResponseDto>(src.Reservation.ReservationPositions.FirstOrDefault().Room.Hotel)));
            CreateMap<OpinionUpdateRequestDto, Opinion>();


            //Reservation
            CreateMap<ReservationRequestDto, Reservation>();
            CreateMap<Reservation, ReservationResponseDto>();

            CreateMap<ReservationPosition, ReservationResponseDto>().ReverseMap();
        }
    }
}
