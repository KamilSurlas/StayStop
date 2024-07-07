import { Component } from '@angular/core';
import { RoomResponseDto } from '../../models/room-response';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomsService } from '../../services/room.service';
import { HotelsDataService } from '../../services/hotels-data.service';
import { ReservationService } from '../../services/reservation.service';
import { ReservationPositionRequestDto } from '../../models/reservation-position-request';

@Component({
  selector: 'app-room-details',
  templateUrl: './room-details.component.html',
  styleUrl: './room-details.component.css'
})
export class RoomDetailsComponent {
//http://localhost:5080/api/reservation/1/reservationPosition
public error: string | null = null;
public room: RoomResponseDto | null = null;
//public amount 
public roomId: number;
public hotelId:number;

constructor(private route: ActivatedRoute,private roomsService: RoomsService,private router: Router, private hotelService: HotelsDataService, 
  private reservationService: ReservationService){
  this.roomId=Number(this.route.snapshot.params['roomid'])
  this.hotelId=Number(this.route.snapshot.params['hotelid'])
  this.roomsService.getById(this.hotelId, this.roomId).subscribe({
    next: (res) => {
      this.room=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
};
public deleteRoom():void{
  this.roomsService.deleteById(this.hotelId, this.roomId).subscribe({
    next: () => {
      this.router.navigateByUrl(`hotels/${this.hotelId}`);
    },
    error: (deleteError) => {
      this.error = deleteError.error;
    }
  })
}

  bookRoom() {
    if (this.reservationService.reservation?.endDate == null || this.reservationService.reservation?.startDate == null) {
      alert('You did not chose dates')
    }
    else {
      let numberOfAdults: number = this.hotelService.getNumberOfAdults() ?? 0;
      let numberOfKids: number = this.hotelService.getNumberOfKids() ?? 0;
  
      if (numberOfKids > 0 || numberOfAdults > 0) {
        let reservationPositions: ReservationPositionRequestDto[] = [];
        reservationPositions.push({
          numberOfAdults: numberOfAdults,
          numberOfChildren: numberOfKids,
          amount: 1,
          roomId: this.roomId
        });
    
        if (this.reservationService.reservation) {
          this.reservationService.reservation.reservationPositions.push(...reservationPositions);
        }
      }
      
      else {
        alert('You cannot book without at least 1 person');
      }
    }
    
    console.log(this.reservationService);
  }
}
