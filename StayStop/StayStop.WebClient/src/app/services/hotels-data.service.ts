import { Injectable } from '@angular/core';
import { HotelResponseDto } from '../models/hotel-response';
import { PageResult } from '../models/page-result';

@Injectable({
  providedIn: 'root'
})
export class HotelsDataService {
  private data: PageResult<HotelResponseDto> | null = null;
  private searchPhrase: string | null = null;

  setHotelsData(data: PageResult<HotelResponseDto>): void {
    this.data = data;
  }

  getHotelsData(): PageResult<HotelResponseDto> | null {
    return this.data;
  }

  setSearchPhrase(searchPhrase: string) : void {
    this.searchPhrase = searchPhrase;
  }

  getSearchPhrase(): string | null {
    return this.searchPhrase;
  }
}
