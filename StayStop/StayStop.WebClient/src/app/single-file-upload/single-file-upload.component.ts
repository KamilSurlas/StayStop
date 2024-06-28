import { Component, Input } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { throwError } from "rxjs";

@Component({
  selector: "app-single-file-upload",
  templateUrl: "./single-file-upload.component.html",
  styleUrls: ["./single-file-upload.component.css"],
})
export class SingleFileUploadComponent {

  status: "initial" | "uploading" | "success" | "fail" = "initial"; // Variable to store file status
  file: File | null = null; // Variable to store file
  private apiUrl: string = 'http://localhost:5080/api/hotel/';
  constructor(private http: HttpClient) {}
  @Input()
  hotelId!:number;
  @Input()
  roomId :number | null = null;
  ngOnInit(): void {}

  // On file Select
  onChange(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.status = "initial";
      this.file = file;
    }
  }
  private buildUrl():string{
   let url = `${this.apiUrl +this.hotelId}`;
   if(this.roomId != null){
    url += `/room/${this.roomId}`;
   }
   url += '/images/cover';

   return url;
  }
  onUpload() {
    if (this.file) {
      const formData = new FormData();
  
      formData.append('coverImage', this.file, this.file.name);
  
     
      const upload$ = this.http.post(this.buildUrl(), formData);
  
      this.status = 'uploading';
  
      upload$.subscribe({
        next: () => {
          this.status = 'success';
        },
        error: (error: any) => {
          this.status = 'fail';
          return throwError(() => error);
        },
      });
    }
  }
}