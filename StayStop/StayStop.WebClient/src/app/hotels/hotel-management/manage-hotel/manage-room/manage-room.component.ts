import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { RoomsService } from '../../../../services/room.service';
import { RoomResponseDto } from '../../../../models/room-response';

@Component({
  selector: 'app-manage-room',
  templateUrl: './manage-room.component.html',
  styleUrl: './manage-room.component.css'
})
export class ManageRoomComponent {
  public roomId: number;
  public hotelId: number;
  public room: RoomResponseDto | null = null;
  constructor(private route: ActivatedRoute,private roomsService: RoomsService,private router: Router){
    this.hotelId=Number(this.route.snapshot.params['hotelid'])
    this.roomId=Number(this.route.snapshot.params['roomid'])
    this.roomsService.getById(this.hotelId, this.roomId).subscribe({
      next: (res) => {
        this.room=res;
      },
      error: (err) => {
        console.log(err);
      }, 
    })
}
}
