import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { LoginModel } from '../models/login-model';
import { AuthenticatedResponse } from '../models/authenticated-response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  invalidLogin: boolean = false; //dodany !
  credentials: LoginModel = {emailAddress:'', password:''};
  showRegistrationMessage = false;

  constructor(private router: Router, private http: HttpClient, private route: ActivatedRoute) { 
    this.route.queryParams.subscribe(params => {
      if (params['registered'] === 'true') {
        this.showRegistrationMessage = true;
      }
    });
  }

  login = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<AuthenticatedResponse>("http://localhost:5080/api/account/login", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: AuthenticatedResponse) => {
          const token = response.accessToken;
          const refreshToken = response.refreshToken;
          localStorage.setItem("accessToken", token); 
          localStorage.setItem("refreshToken", refreshToken);
          this.invalidLogin = false; 
          this.router.navigate(["/"]);
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      })
    }
  }
}
