import { Component, Input } from '@angular/core';
import { HotelResponseDto } from '../models/hotel-response';

@Component({
  selector: '[app-hotel-row]',
  templateUrl: './hotel-row.component.html',
  styleUrl: './hotel-row.component.css'
})
export class HotelRowComponent {
@Input('app-hotel-row') hotel!: HotelResponseDto;
}
