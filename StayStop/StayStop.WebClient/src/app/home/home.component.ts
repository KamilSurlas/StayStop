import { Component, EventEmitter, Output } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../services/auth.service';
import { HotelsService } from '../services/hotels.service';
import { SortDirection } from '../models/sort-direction';
import { HotelsDataService } from '../services/hotels-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
clearData() {
throw new Error('Method not implemented.');
}
  @Output() submit = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();
  public searchPhrase: string | null = null;
  public numberOfKids: number = 0; 
  public numberOfAdults: number = 0; 
  public numberOfRooms: number = 1; 

  constructor(private authService: AuthService, private hotelsService: HotelsService, private hotelsData: HotelsDataService, private router: Router) { }

  isUserAuthenticated = (): boolean => {
    return this.authService.isUserAuthenticated();
  }

  logOut = () => {
    this.authService.logOut();
  }

  public search():void {
    this.hotelsService.getAll({ pageSize: 5, pageNumber: 1, searchPhrase: this.searchPhrase, hotelsSortBy: null, sortDirection: SortDirection.ASC }).subscribe(
      {
      next: (res) => {
        console.log(res)
        this.hotelsData.setHotelsData(res);
        if (this.searchPhrase != null)
          this.hotelsData.setSearchPhrase(this.searchPhrase);
        this.router.navigate(['/hotels']);
      },
      error: (err) => console.log(err)
      });
  }

  public resetForm(): void {
    this.searchPhrase = null;
    this.numberOfKids = 0;
    this.numberOfAdults = 0; 
    this.numberOfRooms = 1;
  }
}
