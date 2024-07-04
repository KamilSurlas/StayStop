import { Component, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { HotelsService } from '../services/hotels.service';
import { SortDirection } from '../models/sort-direction';
import { HotelsDataService } from '../services/hotels-data.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReservationService } from '../services/reservation.service';
import { DatePipe } from '@angular/common'


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
  public minDate: Date = new Date();
  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(private authService: AuthService, private hotelsService: HotelsService, private hotelsData: HotelsDataService, private router: Router, 
    private reservationService: ReservationService) { }

  isUserAuthenticated = (): boolean => {
    return this.authService.isUserAuthenticated();
  }

  logOut = () => {
    this.authService.logOut();
  }

  public search():void {

    if (this.range.get('start')?.value == null || this.range.get('end')?.value == null) {
      this.range.patchValue({
        start: new Date(Date.now()),
        end: new Date(Date.now() + 86400000) // adding 24 hours in milliseconds
      });
    }
   
    const startDateValue = this.range.get('start')?.value;
    const endDateValue = this.range.get('end')?.value;


    this.hotelsService.getAvailable({ 
      pageSize: 5, 
      pageNumber: 1, 
      searchPhrase: this.searchPhrase, 
      hotelsSortBy: null, 
      sortDirection: SortDirection.ASC, 
      stars: null},
      {
      from: startDateValue!,
      to: endDateValue!,
      numOfAdults: this.numberOfAdults,
      numOfChildren: this.numberOfKids
      }
    ).subscribe(
      {
      next: (res) => {
        console.log(res)
        this.hotelsData.setHotelsData(res);
        if (this.searchPhrase != null)
          this.hotelsData.setSearchPhrase(this.searchPhrase);
        this.hotelsData.setNumberOfAdults(this.numberOfAdults);
        this.hotelsData.setNumberOfKids(this.numberOfKids);
        this.hotelsData.setNumberOfRooms(this.numberOfRooms);

        
        
      

        console.log('start' + startDateValue);

        if (startDateValue != null && endDateValue != null) {
          if (!this.reservationService.reservation) {

            //Zmiana czasu, ponieważ roznica czasowa
            const startDateAdjusted = new Date(startDateValue);
            startDateAdjusted.setHours(startDateAdjusted.getHours() + 2);

            const endDateAdjusted = new Date(endDateValue);
            endDateAdjusted.setHours(endDateAdjusted.getHours() + 2);

            this.reservationService.reservation = {
              
              startDate: startDateAdjusted.toISOString(),
              endDate: endDateAdjusted.toISOString(),
              reservationPositions: []
            };


            console.log(this.reservationService.reservation.endDate);
          } else {
            console.log('else');
            this.reservationService.reservation.startDate = startDateValue.toISOString();
            this.reservationService.reservation.endDate = endDateValue.toISOString();
          }
        }
        //console.log(this.reservationService.reservation?.startDate);
        //console.log(this.reservationService.reservation?.endDate);
        //let data = this.datepipe.transform(this.range.get('start')?.value, 'yyyy-MM-dd');
        //console.log(this.range.get('start')?.value?.toISOString());

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
