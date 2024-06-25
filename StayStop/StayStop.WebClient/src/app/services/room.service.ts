import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { RoomResponseDto } from "../models/room-response";
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
}