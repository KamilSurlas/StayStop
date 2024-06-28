import { Component, Input } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { throwError } from "rxjs";

@Component({
  selector: "app-multiple-file-upload",
  templateUrl: "./multiple-file-upload.component.html",
  styleUrls: ["./multiple-file-upload.component.css"],
})
export class MultipleFileUploadComponent {
  status: "initial" | "uploading" | "success" | "fail" = "initial";
  images: File[] = [];
  private apiUrl: string = 'http://localhost:5080/api/hotel/';
  constructor(private http: HttpClient) {}
  @Input()
  hotelId!:number;
  @Input()
  roomId :number | null = null;
  ngOnInit(): void {}

  onChange(event: any) {
    const files = event.target.files;

    if (files.length) {
      this.status = "initial";
      this.images = files;
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
    if (this.images.length) {
      const formData = new FormData();

      [...this.images].forEach((file) => {
        formData.append("images", file, file.name);
      });

      const upload$ = this.http.post(this.buildUrl(), formData);

      this.status = "uploading";

      upload$.subscribe({
        next: () => {
          this.status = "success";
        },
        error: (error: any) => {
          this.status = "fail";
          return throwError(() => error);
        },
      });
    }
  }
}