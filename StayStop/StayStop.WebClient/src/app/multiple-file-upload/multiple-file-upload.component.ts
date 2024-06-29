import { Component, EventEmitter, Input, Output } from "@angular/core";
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
  private apiUrl: string = 'http://localhost:5080/api/images/multiple';
  constructor(private http: HttpClient) {}
  @Output() newImages = new EventEmitter<string[]>();
  ngOnInit(): void {}

  onChange(event: any) {
    const files = event.target.files;

    if (files.length) {
      this.status = "initial";
      this.images = files;
    }
  }
  
  onUpload(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      if (this.images.length) {
        const formData = new FormData();
  
        [...this.images].forEach((file) => {
          formData.append("images", file, file.name);
        });
  
        const upload$ = this.http.post(this.apiUrl, formData);
  
        this.status = "uploading";
  
        upload$.subscribe({
          next: (res: any) => {
            this.newImages.emit(res);
            this.status = "success";
            resolve(); 
          },
          error: (error: any) => {
            this.status = "fail";
            reject(error); 
          }
        });
      } else {
        resolve(); 
      }
    });
  }
  
}