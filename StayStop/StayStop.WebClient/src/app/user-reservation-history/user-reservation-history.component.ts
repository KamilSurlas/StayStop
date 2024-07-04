import { Component } from '@angular/core';
import { ReservationResponseDto } from '../models/reservation-response';
import { ReservationService } from '../services/reservation.service';

@Component({
  selector: 'app-user-reservation-history',
  templateUrl: './user-reservation-history.component.html',
  styleUrl: './user-reservation-history.component.css'
})
export class UserReservationHistoryComponent {

  constructor(private reservationService: ReservationService) {
    this.loadData();
  }
  reservationPositions: ReservationResponseDto[] = [];
  
  loadData() {
    this.reservationService.get().subscribe({
      next: (res) => {
        this.reservationPositions = res;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  canBeCancelled(reservation: ReservationResponseDto) : boolean {
    const castedDate = new Date(reservation.startDate);
    //console.log('casred ' + castedDate);
    //console.log('nowa ' + new Date());
    return new Date() < castedDate;
  }


  cancelReservation(reservationId: number) {
    this.reservationService.delete(reservationId).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      }
    });
    //this.loadData();
  }


  viewDetails(reservation: ReservationResponseDto) {
    //this.roomService
  }
  
}
