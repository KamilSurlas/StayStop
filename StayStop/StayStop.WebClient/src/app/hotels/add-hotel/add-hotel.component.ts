import { Component, EventEmitter, Output, ViewChild, inject } from '@angular/core';
import { HotelRequestDto } from '../../models/hotel-request';
import { HotelsService } from '../../services/hotels.service';
import { HotelType } from '../../models/hotel-type';
import { Router } from '@angular/router';
import { SingleFileUploadComponent } from '../../single-file-upload/single-file-upload.component';
import { MultipleFileUploadComponent } from '../../multiple-file-upload/multiple-file-upload.component';

@Component({
  selector: 'app-add-hotel',
  templateUrl: './add-hotel.component.html',
  styleUrl: './add-hotel.component.css'
})
export class AddHotelComponent {
    @ViewChild('coverImageUpload') coverImageUploadForm!: SingleFileUploadComponent;
    @ViewChild('imagesUpload') imagesUploadForm!: MultipleFileUploadComponent;
  public newHotel: HotelRequestDto = {
    hotelType: null,
    stars: null,
    country: null,
    city: null,
    street: null,
    zipCode: null,
    emailAddress: null,
    phoneNumber: null,
    name: null,
    description: null,
    images: null,
    coverImage: null
  };

  addNewCoverImage(event: string) {
    this.newHotel.coverImage = event;
  }
  public cancelForm() {
    this.newHotel.city = null;
    this.newHotel.stars = null;
    this.newHotel.country = null;
    this.newHotel.description = null;
    this.newHotel.emailAddress = null;
    this.newHotel.hotelType = null;
    this.newHotel.name = null;
    this.newHotel.phoneNumber = null;
    this.newHotel.street = null;
    this.newHotel.zipCode = null;
    this.newHotel.coverImage = null;
    this.newHotel.images = [];
  }
  addNewImages(event: string[]) {
    this.newHotel.images = event;
  }
  private readonly api = inject(HotelsService);
  private readonly router = inject(Router);

  public async onSubmit(): Promise<void> {
    try {
        await this.coverImageUploadForm.onUpload();
        await this.imagesUploadForm.onUpload();
  
        this.api.post(this.newHotel).subscribe({
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
