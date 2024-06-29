// import { Component, EventEmitter, Output, inject } from '@angular/core';
// import { HotelRequestDto } from '../../models/hotel-request';
// import { HotelsService } from '../../services/hotels.service';
// import { HotelType } from '../../models/hotel-type';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-add-hotel',
//   templateUrl: './add-hotel.component.html',
//   styleUrl: './add-hotel.component.css'
// })
// export class AddHotelComponent {
//   coverImage: File | null = null;
//   images: File[] = [];

//   public newHotel: HotelRequestDto = {
//     hotelType: null,
//     stars: null,
//     country: null,
//     city: null,
//     street: null,
//     zipCode: null,
//     emailAddress: null,
//     phoneNumber: null,
//     name: null,
//     description: null,
//     images: this.images,
//     coverImage: this.coverImage!
//   };

//   onCoverImageSelected(event: any) {
//     const file = event.target.files[0];
//     if (file) {
//       this.coverImage = file;
//     }
//   }

//   onImagesSelected(event: any) {
//     const files = event.target.files;
//     if (files) {
//       this.images = Array.from(files);
//     }
//   }

//   public cancelForm() {
//     this.newHotel.city = null;
//     this.newHotel.stars = null;
//     this.newHotel.country = null;
//     this.newHotel.description = null;
//     this.newHotel.emailAddress = null;
//     this.newHotel.hotelType = null;
//     this.newHotel.name = null;
//     this.newHotel.phoneNumber = null;
//     this.newHotel.street = null;
//     this.newHotel.zipCode = null;
//     this.coverImage = null;
//     this.images = [];
//   }

//   private readonly api = inject(HotelsService);
//   private readonly router = inject(Router);

//   public onSubmit(): void {
//     const formData = new FormData();
//     formData.append('hotelType', this.newHotel.hotelType as any);
//     formData.append('stars', this.newHotel.stars!.toString());
//     formData.append('country', this.newHotel.country!);
//     formData.append('city', this.newHotel.city!);
//     formData.append('street', this.newHotel.street!);
//     formData.append('zipCode', this.newHotel.zipCode!);
//     formData.append('emailAddress', this.newHotel.emailAddress!);
//     formData.append('phoneNumber', this.newHotel.phoneNumber!);
//     formData.append('name', this.newHotel.name!);
//     formData.append('description', this.newHotel.description!);

//     if (this.coverImage) {
//       formData.append('coverImage', this.coverImage);
//     }

//     this.images.forEach((file, index) => {
//       formData.append('images', file);
//     });

//     this.api.post(formData).subscribe({
//       next: (res: any) => {
//         const hotelId = res.Id;
//         this.router.navigateByUrl(`management/hotels/`, hotelId);
//       },
//       error: (err) => console.error(err)
//     });
//   }
// }
