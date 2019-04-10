import { Component, OnInit, Input } from '@angular/core';
import { ForumService } from '../_services/forum.service';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  @Input() idLecture:number;
  messages: any[]=[{
    "userName":"Ушастый",
    "text":"Привет привет Винни. Не называй меня ушастым. Я – толстый!"
  }];

  constructor(private forumService:ForumService) { }

  ngOnInit() {
    this.getMessages();
  }

  getMessages(){
    this.forumService.getAllByLectureId(this.idLecture)
      .subscribe((data:any)=>this.messages=data);
  }

}
