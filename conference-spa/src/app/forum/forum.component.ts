import { Component, OnInit, Input } from '@angular/core';
import { ForumService } from '../_services/forum.service';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Message } from '../message';
import { AuthService } from '../_services/auth.service';
import { switchMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';


@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  @Input() idLecture:number;

  text = new FormControl('');
  messages: Message[]=[];

  constructor(private forumService:ForumService,
    private authService:AuthService) { }

  ngOnInit() {
    this.getMessages();
  }

  getMessages(){
    this.forumService.getAllByLectureId(this.idLecture)
      .pipe(
        switchMap(data=> {
          this.messages = data;
          if(this.messages.some(m=> m.isNew))
            return this.forumService.deleteNotifications(this.idLecture);
          return of();
        })
      ).subscribe();
  }

  isEmpty():boolean{
    return this.messages.length===0;
  }

  addMessage(){
    let body = new Message;
    body.text=this.text.value;
    body.applicationId = this.idLecture;

    this.forumService.add(body)
      .subscribe(res=>{
        this.getMessages();
        this.text.reset();
      });

    
  }

  isAuthenticated(){
    return this.authService.isAuthenticated();
  }
}
