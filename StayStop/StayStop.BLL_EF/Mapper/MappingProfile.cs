using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using StayStop.BLL.DateTimeExtension;
using StayStop.BLL.Dtos;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Opinion;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.IService;
using StayStop.BLL_EF.Service;
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
            CreateMap<List<string>?, List<string>>().ConvertUsing((src, dest) => src is null || src.IsNullOrEmpty() ? dest : src);
            CreateMap<string?, string>().ConvertUsing((src, dest) => src is null || src.Length == 0 ? dest : src);
            CreateMap<int?,int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<RoomType?, RoomType>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<HotelType?, HotelType>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

            //User
            CreateMap<User, UserResponseDto>();
            CreateMap<UserRegisterDto, User>().ConvertUsing<RegisterUserConverter>();

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
            CreateMap<OpinionRequestDto, Opinion>().ReverseMap();


            //Reservation
            CreateMap<ReservationRequestDto, Reservation>();
            CreateMap<Reservation, ReservationResponseDto>();

            // ReservationPosition
            CreateMap<ReservationPosition, ReservationPositionResponseDto>();
            CreateMap<ReservationPositionRequestDto, ReservationPosition>();


            //UpdateAcc
            CreateMap<UserUpdateRequestDto, User>()
                .ConvertUsing<UpdateUserConverter>();
        }
    }
}
