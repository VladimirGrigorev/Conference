import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CONFERENCES_ENDPOINT } from '../conf-endpoints';
import { Observable } from 'rxjs';
import { Conference } from '../conference';

@Injectable({
  providedIn: 'root'
})
export class ConferencesService {
  
  private headers: HttpHeaders;
  private baseUrl = CONFERENCES_ENDPOINT;

  constructor(private http: HttpClient) { 
    this.headers = new HttpHeaders({'Content-Type': 'application/json;  charset=utf-8'});
  }

  public getAll(){
    return this.http.get(this.baseUrl, {headers: this.headers});
  }

  public get(id: number){
    return this.http.get(this.baseUrl+'/'+id, {headers: this.headers});
  }

  public add(body){
    return this.http.post(this.baseUrl, body, {headers: this.headers});
  }

  public remove(){
    return this.http.delete(this.baseUrl, {headers: this.headers});
  }

  public update(body){
    return this.http.post(this.baseUrl+"/update", body, {headers: this.headers})
  }

  public getAllWithSections(): Observable<Conference[]>{
    return this.http.get<Conference[]>(this.baseUrl, {headers: this.headers});
  }

  public deleteById(id: number) {
    const params = new HttpParams()
      .set('conferenceId', id.toString());
    return this.http.get<string>(this.baseUrl+"/delete", {params: params});
  }
}

