import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ConferencesService {
  private headers: HttpHeaders;
  private baseUrl: string = 'http://localhost:5000/api/conference';

  constructor(private http: HttpClient) { 
    this.headers = new HttpHeaders({'Content-Type': 'application/json;  charset=utf-8'});
  }

  public getAll(){
    return this.http.get(this.baseUrl, {headers: this.headers});
  }

  public get(id: number){
    return this.http.get(this.baseUrl+'/'+id, {headers: this.headers});
  }
}
