import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  constructor(private router:Router, private authService: AuthService, private http: HttpClient){}
  
  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const userRole = this.authService.getUserRole();
    const path = state.url;

    let canActivate: boolean = false;

    // Sprawdzenie autoryzacji dla poszczególnych ścieżek
    switch(path) {
      case 'management/hotels/add':
        canActivate = userRole === 'Admin' || userRole === 'HotelOwner';
        break;
      
      case 'account':
        canActivate = this.authService.isUserAuthenticated();
        break;

      case 'management/hotels/:hotelid/rooms/add':
        canActivate = userRole === 'Admin' || userRole === 'HotelOwner' || userRole === 'Manager';
        break;

      case 'management/hotels':
        canActivate = userRole === 'Admin' || userRole === 'HotelOwner' || userRole === 'Manager';
        break;

      case 'management/hotels/:hotelid':
        canActivate = userRole === 'Admin' || userRole === 'HotelOwner' || userRole === 'Manager';
        break;

      case 'management/hotels/:hotelid/rooms/:roomid':
        canActivate = userRole === 'Admin' || userRole === 'HotelOwner' || userRole === 'Manager';
        break;

      case 'basket':
        canActivate = this.authService.isUserAuthenticated();
        break;

      case 'history':
        canActivate = this.authService.isUserAuthenticated();
        break;

      case 'history/:reservationid':
        canActivate = this.authService.isUserAuthenticated();
        break;

      case 'history/:reservationid/opinions/add':
        canActivate = this.authService.isUserAuthenticated();
        break;

      case 'opinions/:opinionid/update':
        canActivate = this.authService.isUserAuthenticated();
        break;

      case 'panel':
        canActivate = userRole === 'Admin';
        break;

      case 'panel/:reservationid':
        canActivate = userRole === 'Admin';
        break;

      default:
        canActivate = false;
        break;
    }

    if (!canActivate) {
      this.router.navigate(["login"]);
    }
    return canActivate;
  }
}