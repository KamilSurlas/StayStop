import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  title = 'StayStop.WebClient';

  constructor(private authService: AuthService) {}

  isUserAuthenticated = (): boolean => {
    return this.authService.isUserAuthenticated();
  }

  logOut() {
    this.authService.logOut();
  }
}
