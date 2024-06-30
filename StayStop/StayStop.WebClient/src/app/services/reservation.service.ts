import { Injectable } from '@angular/core';
import { ReservationRequestDto } from '../models/reservation-request';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private httpClient: HttpClient) { }
  public reservation : ReservationRequestDto | null = null;
}
