import { Component } from '@angular/core';
import { ReservationResponseDto } from '../models/reservation-response';
import { ReservationService } from '../services/reservation.service';
import { Router } from '@angular/router';
import { ReservationStatus } from '../models/reservation-status';

@Component({
  selector: 'app-user-reservation-history',
  templateUrl: './user-reservation-history.component.html',
  styleUrl: './user-reservation-history.component.css'
})
export class UserReservationHistoryComponent {

  constructor(private reservationService: ReservationService, private router: Router) {
    this.loadData();
  }
  reservations: ReservationResponseDto[] = [];
  
  loadData() {
    this.reservationService.get().subscribe({
      next: (res) => {
        this.reservations = res;
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


  viewDetails(reservationId: number) {
    this.router.navigateByUrl(`history/${reservationId}`);
  }
  cancelReservation(reservationId: number):void{
    this.reservationService.changeStatus(reservationId,ReservationStatus.Canceled).subscribe({
      next: () => {
        this.loadData();
      },
      error: (err) => {
        console.log(err);
      }
    })
  }
}
