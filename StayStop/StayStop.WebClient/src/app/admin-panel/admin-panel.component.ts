import { Component } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ReservationResponseDto } from '../models/reservation-response';
import { SortDirection } from '../models/sort-direction';
import { PageEvent } from '@angular/material/paginator';
import { PageResult } from '../models/page-result';
import { Router } from '@angular/router';
import { ReservationStatus } from '../models/reservation-status';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent {
  constructor(private reservationService: ReservationService,private router: Router){
    this.loadData();
  }

  result: PageResult<ReservationResponseDto> | null = null;
  public pageNumber: number = 1;
  public pageSize: number = 5;
  public sortBy: string | null = null;
  public pageSizeOptions: number[] = [5,10,15];
  public sortDirection: SortDirection = SortDirection.ASC;
  loadData() {
    this.reservationService.getAll({pageNumber: this.pageNumber, pageSize: this.pageSize, reservationsSortBy: this.sortBy, sortDirection: this.sortDirection }).subscribe({
      next: (res) => {
        this.result = res;
      },
      error: (err) => {
        console.log(err)
      }
    });
  
  }
  isDataEmpty(): boolean {
    if(this.result?.items){
    return this.result.items.length <= 0;
    }
    return false;
  }

  viewDetails(reservationId: number) {
    this.router.navigateByUrl(`panel/${reservationId}`); 
    }
  cancelReservation(reservationId: number) {
    this.reservationService.changeStatus(reservationId,ReservationStatus.Canceled).subscribe({
      next:() => {
        this.loadData();
      },
      error: (err) => {
        console.log(err);
      }
    })
  }
    deleteReservation(reservationId: number) {
      this.reservationService.delete(reservationId).subscribe({
        next:() => {
          this.loadData();
        },
        error: (err) => {
          console.log(err);
        }
      })
    }
    public handlePageEvent(e: PageEvent) {
      this.result!.totalItemsCount = e.length;
      this.pageSize = e.pageSize;
      this.pageNumber = e.pageIndex + 1;
      this.loadData();
    }
}
