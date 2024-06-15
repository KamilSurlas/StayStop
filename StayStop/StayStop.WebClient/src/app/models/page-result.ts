export interface PageResult<T> {
    items: T[];
    totalPages: number;
    itemFrom: number;
    itemTo: number;
    totalItemsCount: number;
}