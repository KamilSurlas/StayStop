import { SortDirection } from "./sort-direction";

export interface HotelPagination {
    searchPhrase: string | null;
    pageNumber: number;
    pageSize: number;
    hotelsSortBy: string | null;
    sortDirection: SortDirection;
}