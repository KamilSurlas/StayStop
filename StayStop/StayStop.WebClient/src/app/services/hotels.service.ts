import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { HotelPagination } from "../models/hotel-pagiantion";
import { Observable } from "rxjs";
import { PageResult } from "../models/page-result";
import { HotelResponseDto } from "../models/hotel-response";
import { Injectable } from "@angular/core";
import { UserResponseDto } from "../models/user-response";
import { HotelUpdateRequestDto } from "../models/hotel-update-request";
import { HotelRequestDto } from "../models/hotel-request";
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
      public getManagers(hotelId: number):Observable<UserResponseDto[]>{
        return this.httpClient.get<UserResponseDto[]>(`${this.apiUrl + hotelId}/managers`);
      }
      public update(hotelId:number, dto: HotelUpdateRequestDto):Observable<void>{
        return this.httpClient.put<void>(this.apiUrl + hotelId, dto);
      }
      public removeManagerFromHotel(hotelId: number, managerId:number):Observable<void>{
        return this.httpClient.delete<void>(`apiUrl/${hotelId}/managers/remove/${managerId}`);
      }
      assignManagerToHotel(hotelId: number, email: string): Observable<void> {
        const url = `${this.apiUrl}${hotelId}/managers`;
        const headers = new HttpHeaders({
          'Content-Type': 'application/json',
        });
        const body = JSON.stringify(email);
        return this.httpClient.post<void>(url, body, { headers });
      }    
      post(hotelData: FormData): Observable<any> {
        return this.httpClient.post<any>(`${this.apiUrl}`, hotelData);
      }
    }