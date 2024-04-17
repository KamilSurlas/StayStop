using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Pagination
{
    public class ReservationPagination
    { 
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? ReservationsSortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
