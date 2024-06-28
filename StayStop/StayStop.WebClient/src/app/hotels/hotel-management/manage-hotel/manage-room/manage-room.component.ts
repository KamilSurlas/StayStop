import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { RoomsService } from '../../../../services/room.service';
import { RoomResponseDto } from '../../../../models/room-response';
import { RoomUpdateRequestDto } from '../../../../models/room-update-request';

@Component({
  selector: 'app-manage-room',
  templateUrl: './manage-room.component.html',
  styleUrl: './manage-room.component.css'
})
export class ManageRoomComponent {
  public roomId: number;
  public hotelId: number;
  public room: RoomResponseDto | null = null;
  public chooseCoverImage: boolean = false;
  public chooseImages: boolean = false;
  constructor(private route: ActivatedRoute,private roomsService: RoomsService,private router: Router){
    this.hotelId=Number(this.route.snapshot.params['hotelid'])
    this.roomId=Number(this.route.snapshot.params['roomid'])
    this.loadRoom();
}
public resetForm(): void {
  this.loadRoom();
}
private loadRoom(){
  this.roomsService.getById(this.hotelId, this.roomId).subscribe({
    next: (res) => {
      this.room=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
}
public onImageDelete(imageToDeletePath: string) {
  this.roomsService.removeImage(imageToDeletePath,this.hotelId, this.roomId).subscribe({
    next: () => {
      this.loadRoom();
    },
    error: (err) => {
      console.log(err);
    }
  })

 
}
public onChooseCoverImage() {
  this.chooseCoverImage=!this.chooseCoverImage;
  this.loadRoom();
}
public onChooseImages() {
  this.chooseImages=!this.chooseImages;
  this.loadRoom();
}
public editRoom():void {
  this.roomsService.update(this.hotelId,this.roomId,this.mapRoomToUpdateDto(this.room!)).subscribe({
    next: () => {
      this.loadRoom();
    },
    error: (err) => {
      console.log(err);
    }
  })
}
private mapRoomToUpdateDto(room: RoomResponseDto): RoomUpdateRequestDto {
  return {
    roomType: room.roomType,
    priceForAdult: room.priceForAdult,
    priceForChild: room.priceForChild,
    numberOfAdults: room.numberOfAdults,
    numberOfChildren: room.numberOfChildren,
    description: room.description,
    isAvailable:room.isAvailable
  };
}
}
