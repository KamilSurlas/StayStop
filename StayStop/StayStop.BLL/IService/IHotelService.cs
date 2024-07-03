using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Hotel.HotelOpinion;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.Pagination;

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
        void DeleteImage(int hotelId, string path);
        PageResult<HotelResponseDto>? GetAvailable(HotelPagination pagination, DateTime from, DateTime to);
    }
}
