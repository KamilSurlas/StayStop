import { Component, ViewChild, inject } from '@angular/core';
import { RoomRequestDto } from '../../models/room-request';
import { RoomsService } from '../../services/room.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SingleFileUploadComponent } from '../../single-file-upload/single-file-upload.component';
import { MultipleFileUploadComponent } from '../../multiple-file-upload/multiple-file-upload.component';

@Component({
  selector: 'app-add-room',
  templateUrl: './add-room.component.html',
  styleUrl: './add-room.component.css'
})
export class AddRoomComponent {
  private hotelId!: number;
  @ViewChild('coverImageUpload') coverImageUploadForm!: SingleFileUploadComponent;
  @ViewChild('imagesUpload') imagesUploadForm!: MultipleFileUploadComponent;
  public newRoom: RoomRequestDto = {
    roomType: null!,
    priceForAdult: null!,
    priceForChild: null!,
    numberOfAdults: null!,
    numberOfChildren: null!,
    images: null!,
    coverImage: null!,
    description:null!,
    isAvailable:true
  };
  constructor(private route: ActivatedRoute){
    this.hotelId=Number(this.route.snapshot.params['hotelid'])
  }
  private readonly api = inject(RoomsService);
  private readonly router = inject(Router);
  addNewCoverImage(event: string) {
    this.newRoom.coverImage = event;
  }
  public cancelForm() {
    this.newRoom.roomType = null!,
    this.newRoom.numberOfAdults=null!,
    this.newRoom.numberOfChildren=null!,
    this.newRoom.priceForAdult=null!,
    this.newRoom.priceForChild=null!,
    this.newRoom.description=null!,
    this.newRoom.images=null!,
    this.newRoom.coverImage=null!
  } 
  addNewImages(event: string[]) {
    this.newRoom.images = event;
  }
  public async onSubmit(): Promise<void> {
    try {
        await this.coverImageUploadForm.onUpload();
        await this.imagesUploadForm.onUpload();
  
        this.api.post(this.hotelId,this.newRoom).subscribe({
          next: (res: any) => {
            const hotelId = res.Id;
            this.router.navigateByUrl(`management/hotels/${hotelId}`);
          },
          error: (err) => console.error(err)
        });
      } catch (err) {
        console.error('Error during file upload:', err);
      }
  }
}
