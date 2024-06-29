import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private apiUrl: string = 'http://localhost:5080/api/images';
  constructor(private httpClient:HttpClient) { }
  public delete(imgUrl: string):Observable<void>{
    const url = this.apiUrl;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const body = JSON.stringify(imgUrl);
    const options = {
      headers: headers,
      body: body}
    return this.httpClient.delete<void>(url, options);
  }
}
