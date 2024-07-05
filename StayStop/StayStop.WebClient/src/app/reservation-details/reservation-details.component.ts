import { Component } from '@angular/core';
import { ReservationResponseDto } from '../models/reservation-response';
import { ReservationService } from '../services/reservation.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrl: './reservation-details.component.css'
})
export class ReservationDetailsComponent {
public reservation: ReservationResponseDto | null = null;
constructor(private reservationService: ReservationService, private activatedRoute: ActivatedRoute){
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
  // wyslanie opinii 
}
}
