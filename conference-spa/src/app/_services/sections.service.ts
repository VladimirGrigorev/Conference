import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LECTURES_ENDPOINT } from '../conf-endpoints';

@Injectable({
  providedIn: 'root'
})
export class SectionsService {

  private headers: HttpHeaders;
  private baseUrl = LECTURES_ENDPOINT;

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
    return this.http.put(this.baseUrl, body, {headers: this.headers})
  }

}
