import { Component } from '@angular/core';
import { ReservationService } from '../services/reservation.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  constructor(private reservationService: ReservationService) {}

  createReservation() {
    this.reservationService.post().subscribe({
      next: (res) => {
        console.log('wyslaon');
      },
      error: (err) => console.log(err)
      });
  }

}
