import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LECTURES_ENDPOINT } from '../conf-endpoints';
import { Observable, of } from 'rxjs';
import { Lecture } from '../lecture';

@Injectable({
  providedIn: 'root'
})
export class LecturesService {

  private baseUrl = LECTURES_ENDPOINT;

  constructor(private httpClient:HttpClient) { }

  get(id:number): Observable<Lecture>{

    let lec = new Lecture();
    lec.info = "cat";
    lec.topic = "furry";

    return of(lec);
    //return this.httpClient.get<Lecture>(this.baseUrl+id);
  }
}
