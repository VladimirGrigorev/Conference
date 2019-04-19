import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { FILES_ENDPOINT, LECTURES_ENDPOINT } from '../conf-endpoints';
import { ConfFile } from '../conf-file';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private lecturesUrl = LECTURES_ENDPOINT;
  private filesUrl = FILES_ENDPOINT;

  constructor(private httpClient: HttpClient) { }

  getAll(lectureId: number): Observable<ConfFile[]>{
    return this.httpClient.get<ConfFile[]>(`${this.lecturesUrl}/${lectureId}/files`);
  }

  upload(lectureId: number, file) {
    const formData = new FormData();
    //for (let file of files)
    formData.append(file.name, file);

    return this.httpClient.post(`${this.lecturesUrl}/${lectureId}/files`, formData);

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

  delete(id:number){
    return this.httpClient.delete( `${this.filesUrl}/${id}`);
  }

  download(id:number){
    return this.httpClient.get( `${this.filesUrl}/${id}`,
     { responseType: 'blob' as 'json' });
  }
}
