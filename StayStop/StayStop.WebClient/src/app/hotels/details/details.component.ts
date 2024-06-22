import { Component } from '@angular/core';
import { HotelResponseDto } from '../../models/hotel-response';
import { ActivatedRoute, Router } from '@angular/router';
import { HotelsService } from '../../services/hotels.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {
public error: string | null=null;
public hotel: HotelResponseDto | null = null;
public hotelId: number;
constructor(private route: ActivatedRoute,private hotelsService: HotelsService,private router: Router){
  this.hotelId=Number(this.route.snapshot.params['hotelid'])
  this.hotelsService.getById(this.hotelId).subscribe({
    next: (res) => {
      this.hotel=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
}
getStars(starCount: number): any[] {
  return new Array(starCount);
}
public deleteHotel():void{
  this.hotelsService.delete(this.hotelId).subscribe({
    next: () => {
      this.router.navigateByUrl('/hotels');
    },
    error: (deleteError) => {
      this.error = deleteError.error;
    }
  })
}
public onRoomChoosed(roomId:number){
  this.router.navigateByUrl(`/hotels/${this.hotelId}/rooms/${roomId}`);
}
};