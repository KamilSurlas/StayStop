import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  title = 'StayStop';

  constructor(private authService: AuthService) {}

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
}
