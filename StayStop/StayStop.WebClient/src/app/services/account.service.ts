import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserResponseDto } from '../models/user-response';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = `http://localhost:5080/api/account/`;
  constructor(private httpClient: HttpClient) {
  
   }
   public getUserById(userId: number):Observable<UserResponseDto> {
    return this.httpClient.get<UserResponseDto>(this.apiUrl + userId);
    
  }
}
