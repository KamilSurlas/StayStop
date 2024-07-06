import { Component } from '@angular/core';
import { HotelResponseDto } from '../../models/hotel-response';
import { ActivatedRoute, Router } from '@angular/router';
import { HotelsService } from '../../services/hotels.service';
import { HotelsDataService } from '../../services/hotels-data.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {
public hotel: HotelResponseDto | null = null;
public hotelId: number;
constructor(private route: ActivatedRoute,private hotelsData: HotelsDataService, private hotelsService: HotelsService,private router: Router){
  this.hotelId=Number(this.route.snapshot.params['hotelid'])
  if(hotelsData.getHotelsData() != null) {
  var hotels = hotelsData.getHotelsData()?.items;
  var hotelFromData = hotels?.find(h=>h.hotelId == this.hotelId);
  this.hotel = hotelFromData!;
  } else {
  this.hotelsService.getById(this.hotelId).subscribe({
    next: (res) => {
      this.hotel=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
}
}
getStars(starCount: number): any[] {
  return new Array(starCount);
}
SeeOpinions(hotelId:number){
  this.router.navigateByUrl(`hotels/${hotelId}/opinions`);
}
public onRoomChoosed(roomId:number){
  this.router.navigateByUrl(`/hotels/${this.hotelId}/rooms/${roomId}`);
}
};