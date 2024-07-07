import { Injectable } from '@angular/core';
import { HotelResponseDto } from '../models/hotel-response';
import { PageResult } from '../models/page-result';

@Injectable({
  providedIn: 'root'
})
export class HotelsDataService {
  private data: PageResult<HotelResponseDto> | null = null;
  private searchPhrase: string | null = null;
  private numberOfAdults: number | null = null;
  private numberOfKids: number | null = null;
  private numberOfRooms: number | null = null;
  private numberOfNights: number | null = null;

  setHotelsData(data: PageResult<HotelResponseDto>): void {
    this.data = data;
  }

  getHotelsData(): PageResult<HotelResponseDto> | null {
    return this.data;
  }

  setNumberOfNights(nOfNights: number):void{
    this.numberOfNights = nOfNights;
  }
  getNumberOfNights():number | null{
  return this.numberOfNights
  }
  setSearchPhrase(searchPhrase: string) : void {
    this.searchPhrase = searchPhrase;
  }

  getSearchPhrase(): string | null {
    return this.searchPhrase;
  }



  setNumberOfAdults(numberOfAdults: number): void {
    this.numberOfAdults = numberOfAdults;
  }

  getNumberOfAdults(): number | null {
    return this.numberOfAdults;
  }


  setNumberOfKids(numberOfKids: number): void {
    this.numberOfKids = numberOfKids;
  }
  getNumberOfKids(): number | null {
    return this.numberOfKids;
  }


  setNumberOfRooms(numberOfRooms: number): void {
    this.numberOfRooms = numberOfRooms;
  }
  getNumberOfRooms(): number | null {
    return this.numberOfRooms;
  }
}
