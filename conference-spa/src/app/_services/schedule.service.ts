import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SCHEDULE_ENDPOINT } from '../conf-endpoints';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private baseUrl = SCHEDULE_ENDPOINT;

  constructor(private http: HttpClient) { }

  get(){
    return this.http.get(this.baseUrl);
  }

  removeLecture(id:number){
    return this.http.delete(this.baseUrl+'/'+id);
  } 

  addLecture(id:number){
    return this.http.post(this.baseUrl,id);
  }
}
