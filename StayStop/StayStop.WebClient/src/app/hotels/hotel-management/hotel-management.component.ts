import { Component } from '@angular/core';
import { HotelResponseDto } from '../../models/hotel-response';
import { PageResult } from '../../models/page-result';
import { HotelsService } from '../../services/hotels.service';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';
import { SortDirection } from '../../models/sort-direction';

@Component({
  selector: 'app-hotel-management',
  templateUrl: './hotel-management.component.html',
  styleUrls: ['./hotel-management.component.css']
})
export class HotelManagementComponent {
  public pageNumber: number = 1;
  public pageSize: number = 5;
  public result: PageResult<HotelResponseDto> | null = null;
  public pageSizeOptions: number[] = [5, 10, 15];
  constructor(private hotelsService: HotelsService, private router: Router) {
    this.loadHotels();
  }

  private loadHotels(): void {
    this.hotelsService.getAll({
      pageSize: this.pageSize,
      pageNumber: this.pageNumber,
      searchPhrase: null,
      hotelsSortBy: null,
      sortDirection: SortDirection.ASC
    }).subscribe({
      next: (res) => {
        this.result = res;
      },
      error: (err) => console.log(err)
    });
  }
  
  public manageHotel(hotelId: number):void{
    this.router.navigateByUrl(`management/hotels/${hotelId}`);
  }

  public addNewHotel() {
    this.router.navigateByUrl('management/hotels/add');
  }
  public handlePageEvent(e: PageEvent) {
    this.result!.totalItemsCount = e.length;
    this.pageSize = e.pageSize;
    this.pageNumber = e.pageIndex + 1;
    this.loadHotels();
  }
}
