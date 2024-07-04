import { Component } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { RoomsService } from '../services/room.service';
import { RoomResponseDto } from '../models/room-response';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  constructor(public reservationService: ReservationService, public roomService: RoomsService) {
    this.loadRoomsDetails();
    this.startDate = this.reservationService.reservation?.startDate;
    this.endDate = this.reservationService.reservation?.endDate;
  }
  public rooms: RoomResponseDto[] = [];
  startDate: string | undefined;
  endDate: string | undefined;
  price: number = 0.0;

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

  createReservation() {
    this.reservationService.post().subscribe({
      next: (res) => {
        console.log('wyslano');
      },
      error: (err) => console.log(err)
      });
  }

}
