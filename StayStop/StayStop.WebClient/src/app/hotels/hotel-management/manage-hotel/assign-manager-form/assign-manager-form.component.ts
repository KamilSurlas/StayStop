import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { HotelsService } from '../../../../services/hotels.service';

@Component({
  selector: 'app-assign-manager-form',
  templateUrl: './assign-manager-form.component.html',
  styleUrl: './assign-manager-form.component.css'
})
export class AssignManagerFormComponent {
@Input() hotelId: number | null = null;
@Output() submit = new EventEmitter<void>();
@Output() cancel = new EventEmitter<void>();

public email!: string;

private readonly api = inject(HotelsService);

public onSubmit(event: any):void{
  this.api.assignManagerToHotel(this.hotelId!,this.email).subscribe({
    next: () => {
     this.submit.emit();
    },
    error: (err) => {
      console.log(err);
    }
  })
}
}
