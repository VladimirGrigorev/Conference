import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {User} from 'src/app/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';
  
  private token: string;

  private tokenKey = 'token';
  
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
}

