import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { HotelsService } from '../../../services/hotels.service';
import { RoomResponseDto } from '../../../models/room-response';
import { HotelResponseDto } from '../../../models/hotel-response';
import { UserResponseDto } from '../../../models/user-response';
import { RoomsService } from '../../../services/room.service';


@Component({
  selector: 'app-manage-hotel',
  templateUrl: './manage-hotel.component.html',
  styleUrl: './manage-hotel.component.css'
})
export class ManageHotelComponent {
public hotelId: number;
public rooms: RoomResponseDto[] | null=null;
public hotel: HotelResponseDto | null = null;
public hotelManagers: UserResponseDto[] | null = null;
constructor(private route: ActivatedRoute,private hotelsService: HotelsService,private roomsService: RoomsService,private router: Router){
  this.hotelId=Number(this.route.snapshot.params['hotelid'])
  this.hotelsService.getById(this.hotelId).subscribe({
    next: (res) => {
      this.hotel=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
  this.roomsService.getAllRoomsFromHotel(this.hotelId).subscribe({
    next: (res) => {
      this.rooms = res;
    },
    error: (err) => {
      console.log(err);
    }
  })
  
  this.hotelsService.getManagers(this.hotelId).subscribe({
    next: (res) => {
      this.hotelManagers = res;
    },
    error: (err) => {
      console.log(err);
    }
  })
}
public onManageRoomClicked(roomId: number):void{
  this.router.navigateByUrl(`hotels/management/${this.hotelId}/rooms/${roomId}`);
}
}