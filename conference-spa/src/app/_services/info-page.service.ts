import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InfoPage } from '../info-page';
import { HttpClient } from '@angular/common/http';
import { CONFERENCES_ENDPOINT, INFOPOST_ENDPOINT } from '../conf-endpoints';

@Injectable({
  providedIn: 'root'
})
export class InfoPageService {

  
  private infoPageUrl = INFOPOST_ENDPOINT;
  private conferenceUrl = CONFERENCES_ENDPOINT;

  constructor(private httpClient: HttpClient) { }

  getAll(conferenceId:number): Observable<InfoPage[]>{
    return this.httpClient.get<InfoPage[]>(`${this.conferenceUrl}/${conferenceId}/pages`);
  }

  get(id: number): Observable<InfoPage>{
    return this.httpClient.get<InfoPage>(`${this.infoPageUrl}/${id}`);
  }

  add(conferenceId:number, body: InfoPage): Observable<Number>{
    return this.httpClient.post<Number>(`${this.conferenceUrl}/${conferenceId}/pages`, body);
  }

  delete(id:number){
    return this.httpClient.delete( `${this.infoPageUrl}/${id}`);
  }
}
