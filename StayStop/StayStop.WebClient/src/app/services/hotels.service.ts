import { HttpClient, HttpParams } from "@angular/common/http";
import { HotelPagination } from "../models/hotel-pagiantion";
import { Observable } from "rxjs";
import { PageResult } from "../models/page-result";
import { HotelResponseDto } from "../models/hotel-response";
import { Injectable } from "@angular/core";
@Injectable({
    providedIn: 'root'
  })
export class HotelsService {
    private apiUrl: string = 'http://localhost:5080/api/hotel/';
    constructor(private httpClient: HttpClient){}
    public getAll(pagination: HotelPagination): Observable<PageResult<HotelResponseDto>>
    {
        const httpParams=new HttpParams().append("pageNumber",pagination.pageNumber)
        .append("pageSize", pagination.pageSize).append("searchPhrase",pagination.searchPhrase ?? "")
        .append("sortBy",pagination.hotelsSortBy ?? "")
        .append("sortDirection",pagination.sortDirection ?? "");

        const params = httpParams;
        return this.httpClient.get<PageResult<HotelResponseDto>>(this.apiUrl, {params: params});
    }
    public getById(hotelId: number):Observable<HotelResponseDto>{
        return this.httpClient.get<HotelResponseDto>(this.apiUrl + hotelId);
      }
      public delete(hotelId:number):Observable<void>{
        return this.httpClient.delete<void>(this.apiUrl + hotelId);
      }
}