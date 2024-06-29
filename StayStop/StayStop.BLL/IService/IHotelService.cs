using Microsoft.AspNetCore.Http;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Hotel.HotelOpinion;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IHotelService
    {
        int Create(HotelRequestDto hotelDto);
        void Delete(int hotelId);
        PageResult<HotelResponseDto> GetAll(Pagination.HotelPagination pagination);
        HotelResponseDto GetById(int hotelId);
        void Update(int hotelId, HotelUpdateRequestDto hotelDto);

        void AddManager(int hotelId, string managerEmail);
        void RemoveManager(int hotelId, int userId);
        HotelOpinionResponseDto GetOpinion(int hotelId);
        List<UserResponseDto> GetManagers(int hotelId);
    }
}
