import { Component, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { HotelsService } from '../../../services/hotels.service';
import { RoomResponseDto } from '../../../models/room-response';
import { HotelResponseDto } from '../../../models/hotel-response';
import { UserResponseDto } from '../../../models/user-response';
import { RoomsService } from '../../../services/room.service';
import { HotelUpdateRequestDto } from '../../../models/hotel-update-request';
import { ImageService } from '../../../image.service';


@Component({
  selector: 'app-manage-hotel',
  templateUrl: './manage-hotel.component.html',
  styleUrl: './manage-hotel.component.css'
})
export class ManageHotelComponent {
public assignManager: boolean = false;
public chooseCoverImage: boolean = false;
public chooseImages: boolean = false;
public hotelId: number;
public rooms: RoomResponseDto[] | null=null;
public hotel: HotelResponseDto | null = null;
public hotelManagers: UserResponseDto[] | null = null;
private loadHotel(hotelId:number):void {
  this.hotelsService.getById(this.hotelId).subscribe({
    next: (res) => {
      this.hotel=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
}
constructor(private route: ActivatedRoute,private hotelsService: HotelsService,private roomsService: RoomsService,private router: Router,private imageService:ImageService){
  this.hotelId=Number(this.route.snapshot.params['hotelid'])
  this.loadHotel(this.hotelId);
  
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
  this.router.navigateByUrl(`management/hotels/${this.hotelId}/rooms/${roomId}`);
}
public resetForm(): void {
  this.loadHotel(this.hotelId);
}
public editHotel():void {
  this.hotelsService.update(this.hotelId,this.mapHotelToUpdateDto(this.hotel!)).subscribe({
    next: () => {
      console.log(this.hotel)
      this.loadHotel(this.hotelId);
    },
    error: (err) => {
      console.log(err);
    }
  })
}

public onImageDelete(imageToDeletePath: string) {
  this.hotelsService.deleteImage(this.hotelId,imageToDeletePath).subscribe({
    next: () => {
      this.loadHotel(this.hotelId);
    },
    error: (err) => {
    
      console.log(err);
    }
  })
  this.imageService.delete(imageToDeletePath);

}
public onChooseImages() {
    this.chooseImages=!this.chooseImages;
    this.loadHotel(this.hotelId);
}
public onAssignManagerClicked():void{
  this.assignManager=!this.assignManager;
}
public removeManagerFromHotel(managerId: number):void{
  this.hotelsService.removeManagerFromHotel(this.hotelId,managerId).subscribe({
    next: () => {
      this.loadHotel(this.hotelId);
    },
    error: (err) => {
      console.log(err);
    }
  })
}
public onCancel():void{
  this.assignManager=!this.assignManager;
}
public onManagerAssigned(): void {
  this.hotelsService.getManagers(this.hotelId).subscribe({
    next: (res) => {
      this.hotelManagers = res;
      this.assignManager=!this.assignManager;
    },
    error: (err) => {
      console.log(err);
    }
  })
}

public onChooseCoverImage() {
  this.chooseCoverImage=!this.chooseCoverImage;
  this.loadHotel(this.hotelId);
}
public saveNewCoverImage(fileName: string): void {

  this.hotel!.coverImage = fileName;
  console.log(fileName);
  this.editHotel();
}
public saveNewImages(fileNames: string[]): void {

  this.hotel!.images = fileNames;
  console.log(fileNames);
  this.editHotel();
}

public deleteHotel() {
  this.hotelsService.delete(this.hotelId).subscribe({
    next: ()=>  {
      this.router.navigate(['management/hotels']);
    },
    error: (err) =>  {
      alert(err);
    }
  })
}
public onAddNewRoomClicked():void{
  this.router.navigateByUrl(`management/hotels/${this.hotelId}/rooms/add`);
}
private mapHotelToUpdateDto(hotel: HotelResponseDto): HotelUpdateRequestDto {
  return {
    hotelType: hotel.hotelType,
    stars: hotel.stars,
    country: hotel.country,
    city: hotel.city,
    street: hotel.street,
    zipCode: hotel.zipCode,
    emailAddress: hotel.emailAddress,
    phoneNumber: hotel.phoneNumber,
    name: hotel.name,
    description: hotel.description,
    coverImage: hotel.coverImage,
    images: hotel.images
  };
}
}