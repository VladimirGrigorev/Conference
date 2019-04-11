import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {User} from 'src/app/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';
<<<<<<< HEAD
constructor(private http: HttpClient) { }
=======
  
  private token: string;
>>>>>>> 2d752f30dac1491f6a3d835ae132a2c1db99b800

  private tokenKey = 'token';
  
  constructor(private http: HttpClient) { }

<<<<<<< HEAD
signin(model: any) { 
=======
  signin(model: any) { // авторизация
    //const body = {email: user.email, password: user.passhash};
>>>>>>> 2d752f30dac1491f6a3d835ae132a2c1db99b800
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

