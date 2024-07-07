import { Component } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ReservationResponseDto } from '../models/reservation-response';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent {
  constructor(private reservationService: ReservationService){
    this.loadData();
  }

  data: ReservationResponseDto[] = [];
  pageNumber: number = 1;
  pageSize: number = 5;

  loadData() {
    this.reservationService.getAll(this.pageNumber, this.pageSize).subscribe({
      next: (res) => {
        this.data = res.items;
      },
      error: (err) => {
        console.log(err)
      }
    });
  }

  isDataEmpty(): boolean {
    return this.data.length <= 0;
  }

  viewDetails(reservationId: number) {
    throw new Error('Method not implemented.');
    }
}
