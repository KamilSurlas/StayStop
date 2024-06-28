import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { RoomResponseDto } from "../models/room-response";
import { RoomUpdateRequestDto } from "../models/room-update-request";
@Injectable({
    providedIn: 'root'
  })
export class RoomsService {
    private apiUrl: string = 'http://localhost:5080/api/hotel/';
    constructor(private httpClient: HttpClient){}
    public getById(hotelId: number, roomId:number):Observable<RoomResponseDto>{
        return this.httpClient.get<RoomResponseDto>(`${this.apiUrl}${hotelId}/room/${roomId}`);
      }
      public deleteById(hotelId: number, roomId:number):Observable<void>{
        return this.httpClient.delete<void>(`${this.apiUrl}${hotelId}/room/${roomId}`);
      }
      public getAllRoomsFromHotel(hotelId: number):Observable<RoomResponseDto[]>{
        return this.httpClient.get<RoomResponseDto[]>(`${this.apiUrl}${hotelId}/room`);
      }
      public update(hotelId:number, roomId: number, dto: RoomUpdateRequestDto):Observable<void>{
        return this.httpClient.put<void>(`${this.apiUrl + hotelId}/room/${roomId}`, dto);
      }
      public removeImage(imageUrl: string, hotelId:number, roomId:number):Observable<void>{
        const url = `${this.apiUrl}${hotelId}/room/${roomId}/images/all`;
        const headers = new HttpHeaders({
          'Content-Type': 'application/json'
        });
        const body = JSON.stringify(imageUrl);
        const options = {
          headers: headers,
          body: body}
        return this.httpClient.delete<void>(url, options);
      }
}