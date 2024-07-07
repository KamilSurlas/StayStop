import { SortDirection } from "./sort-direction";


export interface ReservationPagination {
    pageNumber: number;
    pageSize: number;
    reservationsSortBy: string | null;
    sortDirection: SortDirection;
}