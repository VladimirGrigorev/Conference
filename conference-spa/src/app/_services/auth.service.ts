import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {User} from 'src/app/user';
import { CONFERENCES_ENDPOINT, DOMAIN } from '../conf-endpoints';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';
  
  private token: string;
  private tokenKey = 'token';
  
  private lectures: number[];
  private lecturesKey = 'lectures';

  private isGlobalAdmin: boolean;
  private isGlobalAdminKey = 'admin';

  constructor(private http: HttpClient) { } 


signin(model: any) { 
  return this.http.post(this.baseUrl + 'signin', model)
  .pipe(
    map((response: any) =>{
          const userResponse = response;
          if (userResponse) {
            let t =  userResponse.token;
            localStorage.setItem(this.tokenKey, t );
            this.token = t;

            let l =  userResponse.presentedLectures;
            localStorage.setItem(this.lecturesKey, JSON.stringify(l) );
            this.lectures = l;

            let a =  userResponse.isGlobalAdmin;
            localStorage.setItem(this.isGlobalAdminKey, JSON.stringify(a) );
            this.isGlobalAdmin = a;
          }
        }
    )
  );
  }

  signup(model: any){ //регистрация
    return this.http.post(this.baseUrl+'signup', model);
  }

  isAuthenticated(){
    if (this.token)
      return true;

    if (this.token === undefined){
      let maybeToken = localStorage.getItem(this.tokenKey);
      if (maybeToken){
        this.token = maybeToken;
        return true; 
      }
      this.token = null;
    }

    return false;
  }

  getAuth(){
    return 'Bearer '+this.token;
  }

  isAdmin(){
    if(this.isGlobalAdmin)
      return true;
    
      if(this.isGlobalAdmin === undefined){
        let maybeAdmin = localStorage.getItem(this.isGlobalAdminKey);
        if (maybeAdmin){
          this.isGlobalAdmin = JSON.parse(maybeAdmin);
          return true; 
        }
        this.isGlobalAdmin = false;
      }

      return false;
  }

  isSpeaker(lectureId: number){
    if(this.lectures === undefined){
      let maybeLectures = localStorage.getItem(this.lecturesKey);
      if (maybeLectures){
        this.lectures =  JSON.parse(maybeLectures)        
      }
      else{
        this.lectures = [];
      }
    }
    return this.lectures.includes(lectureId);
  }

  logout() {
    localStorage.clear();
    return this.http.get(DOMAIN);
  }
}

