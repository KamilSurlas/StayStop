using Microsoft.AspNetCore.Http;
using StayStop.BLL.Dtos.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IRoomService
    {
        int Create(int hotelId, RoomRequestDto roomDto);
        void DeleteAll(int hotelId);
        void DeleteById(int hotelId, int roomId);
        IEnumerable<RoomResponseDto> GetAll(int hotelId);
        RoomResponseDto GetById(int hotelId, int roomId);
        RoomResponseDto GetSingleRoomById(int roomId);
        void Update(int hotelId, int roomId, RoomUpdateRequestDto roomDto);
        void SetRoomActivity(int hotelId, int roomId);
        void DeleteImage(int hotelId, int roomId, string path);
    }
}
