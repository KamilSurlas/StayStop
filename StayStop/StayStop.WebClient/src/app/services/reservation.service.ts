import { Injectable } from '@angular/core';
import { ReservationRequestDto } from '../models/reservation-request';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private httpClient: HttpClient) { }
  public reservation : ReservationRequestDto | null = null;
  private apiUrl: string = 'http://localhost:5080/api/reservation/';

  public post(): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}`, this.reservation);
  }
}
