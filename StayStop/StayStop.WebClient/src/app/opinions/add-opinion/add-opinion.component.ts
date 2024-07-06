import { Component } from '@angular/core';
import { ReservationResponseDto } from '../../models/reservation-response';
import { ReservationService } from '../../services/reservation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { OpinionRequestDto } from '../../models/opinion-request';
import { OpinionService } from '../../services/opinion.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-opinion',
  templateUrl: './add-opinion.component.html',
  styleUrl: './add-opinion.component.css'
})
export class AddOpinionComponent {
public reservation: ReservationResponseDto | null = null;
public newOpinion : OpinionRequestDto = {
  mark: null!,
  userOpinion: null!,
  details:null!
}
public marks: number[] = [1,2,3,4,5];
constructor(private reservationService: ReservationService, private route: ActivatedRoute, private opinionService: OpinionService, private router: Router){
  this.loadReservation(this.route.snapshot.params['reservationid'])
}
private loadReservation(reservationId:number):void{
  this.reservationService.getById(reservationId).subscribe({
    next: (res) => {
      this.reservation=res;
    },
    error: (err) => {
      console.log(err);
    }
  })
}
public onCancel(){
  this.newOpinion.details = null!;
  this.newOpinion.mark = null!;
  this.newOpinion.userOpinion=null!;
}

public onSubmit() {
  this.opinionService.add(this.reservation?.reservationId!, this.newOpinion).subscribe({
    next: () => {
      this.router.navigateByUrl(`history/${this.reservation?.reservationId}`);
    },
    error: (err) => {
      console.log(err);
    }
  })
}

}
