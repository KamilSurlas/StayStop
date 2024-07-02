import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private jwtHelper: JwtHelperService) { }
  
  

  isUserAuthenticated(): boolean {
    const token = localStorage.getItem("accessToken");
    
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      //console.log(this.jwtHelper.decodeToken(token))
      return true;
    }
    return false;
  }

  logOut = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.clear();
  }


  getUserName() : string | null {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);

      return (decodeToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
    }
    return null;
  }

  isRoleUser = (): boolean => {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] != "User");
    }
    return false;
  }

  isRoleManager = (): boolean => {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == "Manager");
    }
    return false;
  }
 

  isRoleHotelOwner = (): boolean => {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == "HotelOwner");
    }
    return false;
  }

  isRoleAdmin = (): boolean => {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == "Admin");
    }
    return false;
  }

  getUserEmail() : string | null {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']);
    }
    
    return null;
  }

  getUserLastName() : string | null {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname']);
    }
    
    return null;
  }

  getUserPhoneNumber() : string | null {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone']);
    }
    
    return null;
  }
  getUserId() : string | null {
    const token = localStorage.getItem("accessToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodeToken = this.jwtHelper.decodeToken(token);
      
      return (decodeToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
    }
    
    return null;
  }
}
