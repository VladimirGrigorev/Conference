import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';
import { ConfFile } from '../conf-file';
import { FileService } from '../_services/file.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {

  // public progress: number;
  // public message: string;

  @Input()
  lectureId: number;

  files: ConfFile[];

  constructor(private fileService: FileService,
    private authService: AuthService,
    private http: HttpClient) { }

  ngOnInit(){
    this.getFiles();  
  }

  getFiles(){
    this.fileService.getAll(this.lectureId).subscribe(files=> this.files = files);
  }

  delete(id:number) {
    this.fileService.delete(id)
      .subscribe( resp=> this.getFiles());
  }

  upload(files) {
    if (files.length === 0)
      return;

    this.fileService.upload(this.lectureId, files[0])
      .subscribe( resp=> this.getFiles());

    // const formData = new FormData();

    // for (let file of files)
    //   formData.append(file.name, file);

    // const uploadReq = new HttpRequest('POST', `http://localhost:5000/api/lectures/1/files`, formData, {
    //   reportProgress: true,
    // });

    // this.http.request(uploadReq).subscribe(event => {
    //   if (event.type === HttpEventType.UploadProgress)
    //     this.progress = Math.round(100 * event.loaded / event.total);
    //   else if (event.type === HttpEventType.Response)
    //     this.message = event.body.toString();
    // });
  }

  show(currFile : ConfFile)
  {
    this.fileService.download(currFile.id)
      .subscribe(res=>{ this.downloadConfFile(res, currFile.name)});

    // let filename: string = 'Otchet.doc';

    // this.http.get( 'http://localhost:5000/api/files/1'
    // //, this.getAuthHeader(false, true)
    // , { responseType: 'blob' as 'json' }
    //   ).subscribe(res=>{
    //     this.showFile(res, filename)
    //   })

    // // this.downloadService.getFile(filename).then((result: any) =>
    // // {
    // //   this.showFile(result._body, filename);
    // // });
  }

  // downloadFile(data: Response) {
  //   const blob = new Blob([data], { type: 'text/csv' });
  //   const url= window.URL.createObjectURL(blob);
  //   window.open(url);
  // }

  private downloadConfFile(blob: any, filename: string)
  { 
    if(!blob && !filename)
      return;

    // It is necessary to create a new blob object with mime-type 
    // explicitly set otherwise only Chrome works like it should
    let newBlob = new Blob([blob]);//, { type: "application/vnd.ms-word" });
    // IE doesn't allow using a blob object directly as link href 
    // instead it is necessary to use msSaveOrOpenBlob
    if (window.navigator && window.navigator.msSaveOrOpenBlob)
    {
      window.navigator.msSaveOrOpenBlob(newBlob);
      return;
    }

    // For other browsers: 
    // Create a link pointing to the ObjectURL containing the blob.
    let data = window.URL.createObjectURL(newBlob);
    //window.open(data);
    let link = document.createElement('a');
    link.href = data;
    link.download = filename;
    link.click();
    setTimeout(() =>
    {
      // For Firefox it is necessary to delay revoking the ObjectURL
      window.URL.revokeObjectURL(data);
    }, 200);
  }

  isAddDisplayed(){
    return this.authService.isSpeaker(this.lectureId);
  }
}
