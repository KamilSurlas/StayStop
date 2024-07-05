import { Component } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { RoomsService } from '../services/room.service';
import { RoomResponseDto } from '../models/room-response';
import { HotelsDataService } from '../services/hotels-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  constructor(private reservationService: ReservationService, private roomService: RoomsService, private router: Router) {
    this.loadRoomsDetails();
    this.startDate = this.reservationService.reservation?.startDate;
    this.endDate = this.reservationService.reservation?.endDate;
  }
  public rooms: RoomResponseDto[] = [];
  startDate: string | undefined;
  endDate: string | undefined;
  price: number = 0.0;
  createdMessage: string | null = null;
  createdReservationId: number = null!;
  loadRoomsDetails() {
    const reservationPositions = this.reservationService.reservation?.reservationPositions;

    if (reservationPositions != null) {
      reservationPositions!.forEach(position => {
        if (position.roomId) {
          this.roomService.getSingleRoomById(position.roomId).subscribe({
            next: (res) => {
              res.numberOfAdults = position.numberOfAdults;
              res.numberOfChildren = position.numberOfChildren;
              this.price += res.priceForAdult * res.numberOfAdults;
              this.price += res.priceForChild * res.numberOfChildren;
              this.price *= this.calculateNumberOfNights(new Date(this.startDate!),new Date(this.endDate!));
              this.rooms?.push(res);
            },
            error: (err) => {
              console.log(`Error loading room details for room ID ${position.roomId}:`, err);
            }
          });
        }
      });
    } 
  }
  private calculateNumberOfNights(date1:Date, date2:Date){
    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
    var numberOfNights = Math.ceil(timeDiff / (1000 * 3600 * 24)); 
    console.log(numberOfNights);
    return numberOfNights;
  }
  createReservation() {
    this.reservationService.post().subscribe({
      next: (res) => {
        this.createdReservationId = res.id;
        this.createdMessage = `Your reservation was placed succesfully. Your reservation id: ${this.createdReservationId}`;
      },
      error: (err) => console.log(err)
      });
  }
  seeReservationDetails(){
    this.router.navigateByUrl(`history/${this.createdReservationId}`);
  }
}
