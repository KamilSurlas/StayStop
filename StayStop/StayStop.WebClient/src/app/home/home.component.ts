import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private jwtHelper: JwtHelperService) { }

  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("accessToken");
    console.log(token);
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    return false;
  }

  logOut = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
  }
}
