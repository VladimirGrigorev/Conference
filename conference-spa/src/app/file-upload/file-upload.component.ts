import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FileService } from '../_services/file.service';
import { CheckedStatus } from '../checkedStatus';
import { jsonpCallbackContext } from '@angular/common/http/src/module';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  @Input()
  lectureId: number;

  checkedStatus: CheckedStatus;
  uploadState : UploadState;

  constructor(private fileService: FileService,
    private authService: AuthService) { 
      this.uploadState = UploadState.NotUploaded;
    }

  ngOnInit() {
  }

  upload(files) {
    if (files.length === 0)
      return;

    this.fileService.upload(this.lectureId, files[0])
      .subscribe( 
        resp=> {
          this.uploadState = UploadState.Success;
        },
        (err) => {
          //alert(JSON.stringify(err));
          this.checkedStatus = err.error;
          this.uploadState = UploadState.Failure;
        }
        );
  } 

  goBack(){}

  isFailed(): boolean{
    return this.uploadState == UploadState.Failure;
  }

  isSuccessful(): boolean{
    return this.uploadState == UploadState.Success;
  }

}

enum UploadState {
  NotUploaded,
  Success,
  Failure
}
