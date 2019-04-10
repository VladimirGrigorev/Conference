import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FORUM_ENDPOINT } from '../conf-endpoints';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  private headers: HttpHeaders;
  private baseUrl = FORUM_ENDPOINT;

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
