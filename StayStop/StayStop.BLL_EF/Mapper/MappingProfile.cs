using AutoMapper;
using StayStop.BLL.Dtos;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Opinion;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.Dtos.User;
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
            // Nullable types
            CreateMap<List<string>?, List<string>>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<int?,int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<RoomType?, RoomType>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<HotelType?, HotelType>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

            //User
            CreateMap<User, UserResponseDto>();


            // Hotel
            CreateMap<HotelRequestDto, Hotel>();
            CreateMap<HotelUpdateRequestDto, Hotel>()          
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<Hotel, HotelResponseDto>();


            // Rooms
            CreateMap<RoomRequestDto, Room>();
            CreateMap<RoomUpdateRequestDto, Room>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Room, RoomResponseDto>();


            // Opinion
            CreateMap<RoomRequestDto, Room>();
            CreateMap<Opinion, OpinionResponseDto>()
                  .ForMember(dest => dest.Hotel, opt => opt.MapFrom((src, dest,_, context) =>
                      context.Mapper.Map<HotelResponseDto>(src.Reservation.ReservationPositions.FirstOrDefault().Room.Hotel)));
            CreateMap<OpinionUpdateRequestDto, Opinion>();


            //Reservation
            CreateMap<ReservationRequestDto, Reservation>();
            CreateMap<Reservation, ReservationResponseDto>();

            // ReservationPosition
            CreateMap<ReservationPosition, ReservationPositionResponseDto>();
            CreateMap<ReservationPositionRequestDto,ReservationPosition>();
        }
    }
}
