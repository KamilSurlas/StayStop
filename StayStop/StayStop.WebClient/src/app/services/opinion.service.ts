import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OpinionRequestDto } from '../models/opinion-request';
import { Observable } from 'rxjs';
import { OpinionResponseDto } from '../models/opinion-response';
import { OpinionUpdateRequestDto } from '../models/opinion-update-request';

@Injectable({
  providedIn: 'root'
})
export class OpinionService {

  constructor(private httpClient:HttpClient) { }


  private apiUrl: string = `http://localhost:5080/api/reservation/`;
  public add(reservationId: number, request: OpinionRequestDto):Observable<void>{
  return  this.httpClient.post<void>(`${this.apiUrl + reservationId}/opinion`,request );
  }
  public getUserOpinions():Observable<OpinionResponseDto[]>{
    return this.httpClient.get<OpinionResponseDto[]>(`http://localhost:5080/api/user/opinions`);
  }
  public deleteOpinionById(opinionId: number):Observable<void>{
    return this.httpClient.delete<void>(`http://localhost:5080/api/opinions/${opinionId}`);
  }
  public getOpinionById(opinionId: number):Observable<OpinionResponseDto>{
    return this.httpClient.get<OpinionResponseDto>(`http://localhost:5080/api/opinions/${opinionId}`);
  }
  public update(opinionId: number, updateDto: OpinionUpdateRequestDto):Observable<void>{
    return this.httpClient.put<void>(`http://localhost:5080/api/opinions/${opinionId}`,updateDto);
  }
}
