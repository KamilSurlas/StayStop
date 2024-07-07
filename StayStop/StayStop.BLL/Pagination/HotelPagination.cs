
namespace StayStop.BLL.Pagination
{
    public class HotelPagination
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? HotelsSortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
