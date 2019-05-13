import { Injectable } from '@angular/core';
import { APPLICATION_ENDPOINT } from '../conf-endpoints';
import { Observable } from 'rxjs';
import { Application, ApplicationStatusInfo } from '../application';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  private applicationsUrl = APPLICATION_ENDPOINT;

  constructor(private httpClient: HttpClient) { }

  getMy(): Observable<Application[]>{
    return this.httpClient.get<Application[]>(`${this.applicationsUrl}/my`);
  }

  getConsidered(): Observable<Application[]>{
    return this.httpClient.get<Application[]>(`${this.applicationsUrl}/considered`);
  }

  get(id: number): Observable<Application>{
    return this.httpClient.get<Application>(`${this.applicationsUrl}/${id}`);
  }

  setStatus(id: number, appStatus: ApplicationStatusInfo){
    return this.httpClient.patch(`${this.applicationsUrl}/${id}`, appStatus);
  }

  add(body: Application): Observable<Number>{
    return this.httpClient.post<Number>(this.applicationsUrl, body);
  }

  delete(id:number){
    return this.httpClient.delete( `${this.applicationsUrl}/${id}`);
  }
}
