import { Component, Inject, EventEmitter, OnInit } from '@angular/core';
import { UploadProgress, UploadOutput, UploadInput, UploadFile, humanizeBytes, UploaderOptions, UploadStatus } from 'ngx-uploader';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'image-add',
  templateUrl: 'image-add.component.html'
})
export class ImageAddComponent implements OnInit {
  formData: FormData;
  files: UploadFile[];
  filesNotUploaded: UploadFile[];
  filesUploaded: UploadFile[];
  uploadInput: EventEmitter<UploadInput>;
  humanizeBytes: Function;
  dragOver: boolean;
  options: UploaderOptions;
  urlImageApi: string;
  urlUploadApi: string;
  urlVisionApi: string;
  message: string;

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string)
  {
    this.options = { concurrency: 1, allowedContentTypes: [ "image/jpeg", "image/png", "image/bmp" ] };
    this.files = [];
    this.filesNotUploaded = [];
    this.filesUploaded = [];
    this.uploadInput = new EventEmitter<UploadInput>();
    this.humanizeBytes = humanizeBytes;
    this.urlUploadApi = this.baseUrl + "api/upload";
    this.urlVisionApi = this.baseUrl + "api/googlevisionapi/process";
    this.urlImageApi = this.baseUrl + "api/image";
  }

  ngOnInit() {
    // load items from db
    this.http.get<ImageModelWithDetailsResult[]>(this.urlImageApi).subscribe(res => {
      let filesUploaded: UploadFile[] = [];
        res.forEach(function (elem) {
          let file = <UploadFile>{};
          file.id = elem.Id.toString();
          file.name = elem.Name;
          let progress = <UploadProgress>{};
          progress.status = UploadStatus.Done;
          file.progress = progress;
          file.response = elem;
          filesUploaded.push(file);
        });
      this.filesUploaded = filesUploaded;
      },
      error => {
        console.error(error);
      });
  }

  updateNotUploadFiles() {
    this.filesNotUploaded = this.files.filter(file => file.progress.status !== UploadStatus.Done);
  }

  onUploadOutput(output: UploadOutput): void {
    if (output.type === 'done') {
      this.message = "";
      if (output.file.responseStatus === 200) {
        if (output.file.response.Error !== '') {
          output.file.progress.status = UploadStatus.Queue;
          console.log("File index=" + output.file.id + " " + output.file.name + " is uploaded. Invoking api/googlevisionapi/process");
          this.http.post(this.urlVisionApi, output.file.response).subscribe(res => {
              output.file.progress.status = UploadStatus.Done;
              output.file.response = res;
              console.log("File index=" + output.file.id + ", fileDbId=" + output.file.response.Id+" "+output.file.name + " processed. GoogleVisionApiDetails: " + output.file.response.GoogleVisionApiDetails);
              this.filesUploaded.push(output.file);
              this.removeFile(output.file.id);
            },
            error => {
              this.message = error.message;
              this.removeFile(output.file.id);
              console.error(error);
            });
        } else {
          this.message = output.file.response.Error;
        }
      } else {
        this.message = "Upload failed for " + output.file.name +". Try again.";
        this.removeFile(output.file.id);
      }
    } else {
      if (output.type === 'allAddedToQueue') {
        this.startUpload();
      } else if (output.type === 'addedToQueue' && typeof output.file !== 'undefined') {
        this.files.push(output.file);
      } else if (output.type === 'uploading' && typeof output.file !== 'undefined') {
        const index = this.files.findIndex(file => typeof output.file !== 'undefined' && file.id === output.file.id);
        this.files[index] = output.file;
      } else if (output.type === 'removed') {
        this.files = this.files.filter((file: UploadFile) => file !== output.file);
      } else if (output.type === 'dragOver') {
        this.dragOver = true;
      } else if (output.type === 'dragOut') {
        this.dragOver = false;
      } else if (output.type === 'drop') {
        this.dragOver = false;
      } else if (output.type === 'rejected' && typeof output.file !== 'undefined') {
        this.message = output.file.name + ' rejected';
      }
      this.updateNotUploadFiles();
    }
  }

  startUpload(): void {
    
    const event: UploadInput = {
      type: 'uploadAll',
      url: this.urlUploadApi,
      method: 'POST'
    };

    this.uploadInput.emit(event);
  }

  cancelUpload(id: string): void {
    this.uploadInput.emit({ type: 'cancel', id: id });
  }

  removeFile(id: string): void {
    this.uploadInput.emit({ type: 'remove', id: id });
  }

  removeAllFiles(): void {
    this.uploadInput.emit({ type: 'removeAll' });
  }

  onDelete(file: UploadFile) {
    if (confirm("Do you really want to delete this photo " + file.response.Name+" ?")) {
      var url = this.baseUrl + "api/image/" + file.response.Id;
      this.http
        .delete(url)
        .subscribe(res => {
          if (res === "ok") {
            console.log("File index=" + file.id + ", fileDbId=" + file.response.Id + " " + file.response.Name+" deleted.");
            this.filesUploaded.splice(this.filesUploaded.indexOf(file), 1);
          } else {
            console.log("File index=" + file.id + ", fileDbId=" + file.response.Id + " " + file.response.Name +" can not be deleted! Error: "+res);
          }
        }, error => console.log(error));
    }
  }
}
