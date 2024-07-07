import { Component } from '@angular/core';
import { UserResponseDto } from '../../models/user-response';
import { ReservationResponseDto } from '../../models/reservation-response';
import { AccountService } from '../../services/account.service';
import { ActivatedRoute } from '@angular/router';
import { ReservationService } from '../../services/reservation.service';

@Component({
  selector: 'app-admin-reservation-details',
  templateUrl: './admin-reservation-details.component.html',
  styleUrl: './admin-reservation-details.component.css'
})
export class AdminReservationDetailsComponent {
  public booker: UserResponseDto | null = null;
  public reservation: ReservationResponseDto | null = null;
  public reservationId!: number;
  constructor(private accountService: AccountService,private activatedRoute: ActivatedRoute,private reservationService: ReservationService ){
    this.reservationId =Number(this.activatedRoute.snapshot.params['reservationid'])
    this.laodReservation(this.reservationId);
  }
  private laodReservation(reservationId: number) {
    this.reservationService.getById(reservationId).subscribe({
     next:(res) => {
       this.reservation = res;
       this.laodBookerData(this.reservation.userId);
     },
     error: (err) => {
       console.log(err);
     }
    })
   }
   private laodBookerData(bookerId: number) {
     this.accountService.getUserById(bookerId).subscribe({
       next:(res)=>{
         this.booker=res;
       },
       error: (err) =>{ console.log(err);}
     })
   }
   }

