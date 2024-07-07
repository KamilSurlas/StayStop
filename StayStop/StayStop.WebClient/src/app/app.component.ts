import { Component, HostListener } from '@angular/core';
import { AuthService } from './services/auth.service';
import { ReservationService } from './services/reservation.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'StayStop';

  constructor(private authService: AuthService, private reservationService: ReservationService, private router: Router) {}

  isUserAuthenticated = (): boolean => {
    return this.authService.isUserAuthenticated();
  }

  logOut() {
    this.authService.logOut();
  }

  getUserName(): string | null  {
    return this.authService.getUserName();
  }

  isRoleUser = (): boolean => {
    return this.authService.isRoleUser();
  }

  canUserManageHotel = () : boolean => {
    return (this.authService.isRoleAdmin() || this.authService.isRoleHotelOwner() || this.authService.isRoleManager());
  }

  isBasketEmpty = (): boolean => {
    return (ReservationService.reservation?.reservationPositions.length == 0 || ReservationService.reservation == null);
  }

  navigateToCart() {
    this.router.navigate(["basket"]); 
  }

  isUserAdmin(): boolean {
    return this.authService.isRoleAdmin();
  }

  @HostListener('window:beforeunload', ['$event'])
  clearLocalStorage(event: Event) {
    // Usuń tokeny z localStorage
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }
}
