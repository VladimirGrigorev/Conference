import { Injectable } from '@angular/core';
import { USER_ENDPOINT, DOMAIN } from '../conf-endpoints';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = USER_ENDPOINT;
  baseUrlUsers=DOMAIN;
  constructor(private http:HttpClient) { }

  getName(){
    return this.http.get(this.baseUrl+'/myname');
  }

  getUserByEmail(mail:string){
    return this.http.get(this.baseUrlUsers+'/users',{
      params:{
        email:mail
      },
      observe: 'response'
    });
  }
}
