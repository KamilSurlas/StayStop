import { Component } from '@angular/core';
import { OpinionResponseDto } from '../../models/opinion-response';
import { OpinionService } from '../../services/opinion.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-user-opinions',
  templateUrl: './user-opinions.component.html',
  styleUrl: './user-opinions.component.css'
})
export class UserOpinionsComponent {
public opinions: OpinionResponseDto[] = [];
constructor(private opinionService: OpinionService,private router: Router){
this.loadOpinions();
}

private loadOpinions() {
  this.opinionService.getUserOpinions().subscribe({
    next: (res)=>{
      this.opinions=res;
    },
    error: (err) => {
      console.log(err);
    }
  })
}
public deleteOpinion(opinionId: number): void {
  this.opinionService.deleteOpinionById(opinionId).subscribe({
    next: () => {
     this.loadOpinions();
     
    },
    error: (err) => {
      console.log(err);
    }
  });
}

public updateOpinion(opinionId:number){
  this.router.navigateByUrl(`opinions/${opinionId}/update`);
}
}
