import { Injectable } from '@angular/core';
import { ReservationRequestDto } from '../models/reservation-request';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";
import { ReservationResponseDto } from '../models/reservation-response';
import { ReservationStatus } from '../models/reservation-status';
import { PageResult } from '../models/page-result';
import { ReservationPagination } from '../models/reservation-pagination';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  public static reservation : ReservationRequestDto | null = null;
  private apiUrl: string = 'http://localhost:5080/api/reservation/';
  constructor(private httpClient: HttpClient) { 
  }
 

  public post(): Observable<any> {
    ReservationService.reservation!.reservationStatus = ReservationStatus.Booked;
    return this.httpClient.post<any>(`${this.apiUrl}`, ReservationService.reservation);
  }

  public get(): Observable<ReservationResponseDto[]> {
    return this.httpClient.get<ReservationResponseDto[]>(`http://localhost:5080/api/user/reservations/`);
  }

  public delete(reservationId: number): Observable<any> {
    return this.httpClient.delete<any>(`${this.apiUrl}${reservationId}`);
  }
  public getById(reservationId: number):Observable<ReservationResponseDto>{
    return this.httpClient.get<ReservationResponseDto>(`${this.apiUrl}${reservationId}`);
  }
  public changeStatus(reservationId: number, status: ReservationStatus):Observable<void>{
    return this.httpClient.put<void>(this.apiUrl + reservationId,status);
  }

  public getAll(pagination: ReservationPagination):Observable<PageResult<ReservationResponseDto>> {
    const httpParams = new HttpParams()
                        .append("pageNumber", pagination.pageNumber)
                        .append("pageSize", pagination.pageSize)
                        .append("sortBy",pagination.reservationsSortBy ?? "")
                        .append("sortDirection",pagination.sortDirection ?? "");
    return this.httpClient.get<PageResult<ReservationResponseDto>>(this.apiUrl, {params: httpParams});
  }

}
