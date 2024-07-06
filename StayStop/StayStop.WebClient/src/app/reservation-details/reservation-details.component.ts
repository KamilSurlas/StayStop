import { Component } from '@angular/core';
import { ReservationResponseDto } from '../models/reservation-response';
import { ReservationService } from '../services/reservation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReservationStatus } from '../models/reservation-status';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrl: './reservation-details.component.css'
})
export class ReservationDetailsComponent {
public reservation: ReservationResponseDto | null = null;
public status = ReservationStatus;
constructor(private reservationService: ReservationService, private activatedRoute: ActivatedRoute, private router: Router){
  this.loadReservation(this.activatedRoute.snapshot.params['reservationid'])
}
private loadReservation(reservationId:number):void{
  this.reservationService.getById(reservationId).subscribe({
    next: (res) => {
      this.reservation=res;
    },
    error: (err) => {
      console.log(err);
    }
  })
}
isPastEndDate(endDate: string): boolean {
  const endDateAsDate = new Date(endDate);
  const currentDate = new Date();
  return endDateAsDate < currentDate;
}

public addOpinion(reservationId: number) {
  this.router.navigateByUrl(`history/${reservationId}/opinions/add`);
}
}
