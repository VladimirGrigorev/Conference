import { Component, OnInit, Input } from '@angular/core';
import { ForumService } from '../_services/forum.service';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Message } from '../message';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  @Input() idLecture:number;

  text = new FormControl('');
  messages: any[]=[];

  constructor(private forumService:ForumService,
    private authService:AuthService) { }

  ngOnInit() {
    this.getMessages();
  }

  getMessages(){
    this.forumService.getAllByLectureId(this.idLecture)
      .subscribe((data:any)=>this.messages=data);
  }

  isEmpty():boolean{
    return this.messages.length===0;
  }

  addMessage(){
    let body = new Message;
    body.text=this.text.value;
    body.lectureId = this.idLecture;

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
