import { Injectable } from '@angular/core';
import { USER_ENDPOINT } from '../conf-endpoints';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = USER_ENDPOINT;
  constructor(private http:HttpClient) { }

  getName(){
    return this.http.get(this.baseUrl+'/myname');
  }
}
