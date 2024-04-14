﻿using StayStop.BLL.Dtos.Hotel;
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
        PageResult<HotelResponseDto> GetAll(HotelPagination pagination);
        HotelResponseDto GetById(int hotelId);
        public void Update(int hotelId, HotelUpdateRequestDto hotelDto);
    }
}