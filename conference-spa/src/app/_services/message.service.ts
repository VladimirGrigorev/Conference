import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  IDLecture: number;
  constructor() { }

  addIDLecture(id:number){
    this.IDLecture=id;
  }
}
