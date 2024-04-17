
namespace StayStop.BLL.Pagination
{
    public class HotelPagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? HotelsSortBy { get; set; }
        public string? ReservationsSortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
