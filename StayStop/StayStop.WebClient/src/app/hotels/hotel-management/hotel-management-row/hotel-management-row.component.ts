import { Component, Input } from '@angular/core';
import { HotelResponseDto } from '../../../models/hotel-response';

@Component({
  selector: '[app-hotel-management-row]',
  templateUrl: './hotel-management-row.component.html',
  styleUrl: './hotel-management-row.component.css'
})
export class HotelManagementRowComponent {
@Input('app-hotel-management-row') hotel!: HotelResponseDto;
}
