import { Component } from '@angular/core';
import { RegisterModel } from '../models/register-model';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { AuthenticatedResponse } from '../models/authenticated-response';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  credentials: RegisterModel = {email:'', password:'', confirmPassword: '', phoneNumber: '', name: '', lastName: ''};
  invalidRegister: boolean = false;
  validationErrors: any = {};
  constructor(private router: Router, private http: HttpClient) { }

  register = (form: NgForm) => {
    if (form.valid) {
      this.http.post("http://localhost:5080/api/account/register", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json" })
      }).subscribe({
        next: () => { 
          this.invalidRegister = false; 
          this.router.navigate(["/login"], { queryParams: { registered: 'true' } });; 
        },
        error: (err: HttpErrorResponse) => {
          this.invalidRegister = true;
          this.validationErrors = err.error.errors;
          console.log( this.validationErrors);
        }
      });
    }
  }
  
}
