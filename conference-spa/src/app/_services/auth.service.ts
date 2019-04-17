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
  
  private lectures: number[];
  private lecturesKey = 'lectures';

  private isGlobalAdmin: boolean;
  private isGlobalAdminKey = 'admin';

  private subscribedLectures:number[];
  private subscribedLecturesKey = 'subscribedLectures';

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

            let sL =  userResponse.subscribedLectures;
            localStorage.setItem(this.subscribedLecturesKey, JSON.stringify(sL) );
            this.subscribedLectures = sL;

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

  isListener(lectureId: number){
    this.initSubscribedLectures();
    return this.subscribedLectures.includes(lectureId);
  }

  addListener(lectureId:number){
    this.initSubscribedLectures();
    this.subscribedLectures.push(lectureId);
    localStorage.setItem(this.subscribedLecturesKey,JSON.stringify(this.subscribedLectures));
  }

  deleteListener(id:number){
    this.initSubscribedLectures();
    let index = this.subscribedLectures.indexOf(id);
    if(index>-1)
    {
      this.subscribedLectures.splice(index,1);
      localStorage.setItem(this.subscribedLecturesKey,JSON.stringify(this.subscribedLectures));
    }
  }

  initSubscribedLectures(){
    if(this.subscribedLectures === undefined){
      let maybeLectures = localStorage.getItem(this.subscribedLecturesKey);
      if (maybeLectures){
        this.subscribedLectures =  JSON.parse(maybeLectures)        
      }
      else{
        this.subscribedLectures = [];
      }
    }
  }

}

