import { Component } from '@angular/core';
import { OpinionResponseDto } from '../../models/opinion-response';
import { OpinionUpdateRequestDto } from '../../models/opinion-update-request';
import { OpinionService } from '../../services/opinion.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-update-opinion',
  templateUrl: './update-opinion.component.html',
  styleUrl: './update-opinion.component.css'
})
export class UpdateOpinionComponent {
public opinionId!: number; 
private opinion: OpinionResponseDto | null = null;
public opinionUpdateDto!: OpinionUpdateRequestDto;

constructor(private opinionService: OpinionService, private route: ActivatedRoute ){
  this.opinionId = Number(this.route.snapshot.params['opinionid']);
 
  this.loadOpinion();
  console.log(this.opinion);
}
private loadOpinion() {
  this.opinionService.getOpinionById(this.opinionId).subscribe({
    next: (res) => {
      this.opinion = res;
      this.opinionUpdateDto = {
        userOpinion: this.opinion?.userOpinion,
        details: this.opinion?.details,
        mark: this.opinion?.mark
      };
    },
    error: (err) => {
      console.log(err);
    }
  });
}


public onSubmit() {
  this.opinionService.update(this.opinionId,this.opinionUpdateDto).subscribe({
    next: () => {
      this.loadOpinion();
      alert('Opinion updated successfully');
    },
    error: (err) => {
      console.log(err);
    }
  })
}
}
