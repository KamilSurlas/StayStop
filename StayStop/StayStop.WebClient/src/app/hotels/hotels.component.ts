import { Component } from '@angular/core';
import { SortDirection } from '../models/sort-direction';
import { PageResult } from '../models/page-result';
import { HotelResponseDto } from '../models/hotel-response';
import { HotelsService } from '../services/hotels.service';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { HotelsDataService } from '../services/hotels-data.service';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotels.component.html',
  styleUrl: './hotels.component.css'
})
export class HotelsComponent {
  public pageNumber: number = 1;
  public pageSize: number = 5;
  public searchPhrase: string | null = null;
  public sortBy: string | null = null;
  public sortDirection: SortDirection = SortDirection.ASC;
  public result: PageResult<HotelResponseDto> | null = null;
  public numberOfAdults: number | null = null;
  public numberOfKids: number | null = null;
  public numberOfRooms: number | null = null;
  public pageSizeOptions: number[] = [5,10,15];
  public stars: number | null = null;
  constructor(private hotelsService: HotelsService, private router: Router, private hotelsData: HotelsDataService){
    if(hotelsData.getHotelsData() != null) {
      this.result = hotelsData.getHotelsData();
      if(hotelsData.getSearchPhrase() != null)
        this.searchPhrase = hotelsData.getSearchPhrase();
      if(hotelsData.getNumberOfAdults() != null)
        this.numberOfAdults = hotelsData.getNumberOfAdults();
      if(hotelsData.getNumberOfKids() != null)
        this.numberOfKids = hotelsData.getNumberOfKids();
    }
    else
      this.laodHotels();
  }
  private laodHotels():void {
    this.hotelsService.getAll({pageSize: this.pageSize, pageNumber:this.pageNumber, searchPhrase: this.searchPhrase, hotelsSortBy: this.sortBy, sortDirection: this.sortDirection, stars:this.stars}).subscribe(
      {
      next: (res) => {
        this.result= res
      },
      error: (err) => console.log(err)
      });
  }
  public redirectToHotelDetails(hotelId: number): void {
    this.router.navigateByUrl(`/hotels/${hotelId}`);
  }
  public handlePageEvent(e: PageEvent) {
    this.result!.totalItemsCount = e.length;
    this.pageSize = e.pageSize;
    this.pageNumber = e.pageIndex + 1;
    this.laodHotels();
  }
}
