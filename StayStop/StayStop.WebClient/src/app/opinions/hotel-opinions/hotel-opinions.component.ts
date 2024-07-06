import { Component } from '@angular/core';
import { HotelResponseDto } from '../../models/hotel-response';
import { OpinionService } from '../../services/opinion.service';
import { HotelsService } from '../../services/hotels.service';
import { ActivatedRoute } from '@angular/router';
import { OpinionResponseDto } from '../../models/opinion-response';

@Component({
  selector: 'app-hotel-opinions',
  templateUrl: './hotel-opinions.component.html',
  styleUrl: './hotel-opinions.component.css'
})
export class HotelOpinionsComponent {
public hotel: HotelResponseDto | null = null;
public opinions: OpinionResponseDto[] = [];
public hotelId : number = this.route.snapshot.params['hotelid'];
constructor(private hotelService:HotelsService,private route: ActivatedRoute){
  this.loadHotel();
    this.loadOpinions();

}


private loadHotel() {
  this.hotelService.getById(this.hotelId).subscribe({
    next: (res) => {
      this.hotel=res;
    },
    error: (err) => {
      console.log(err);
    }, 
  })
}
private loadOpinions(){
  this.hotelService.getOpinions(this.hotelId).subscribe({
    next: (res) => {
      this.opinions=res;
      console.log(res);
    },
    error: (err) => {
      console.log(err);
    }, 
  })
}
}

