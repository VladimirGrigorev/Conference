import { Component, OnInit } from '@angular/core';
import { ForumService } from '../_services/forum.service';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  messages: any[]=[];

  constructor(private forumService:ForumService,
    private messageService:MessageService) { }

  ngOnInit() {
    this.getMessages();
  }

  getMessages(){
    this.forumService.get(this.messageService.IDLecture)
      .subscribe();
  }

}
