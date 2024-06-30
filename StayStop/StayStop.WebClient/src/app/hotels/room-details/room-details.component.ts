import { Component } from '@angular/core';
import { RoomResponseDto } from '../../models/room-response';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomsService } from '../../services/room.service';
import { HotelsDataService } from '../../services/hotels-data.service';

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
constructor(private route: ActivatedRoute,private roomsService: RoomsService,private router: Router, private hotelService: HotelsDataService){
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
    let numberOfAdults: number | null = this.hotelService.getNumberOfAdults();
    let numberOfKids: number | null = this.hotelService.getNumberOfKids();
    let amount
  }
}
