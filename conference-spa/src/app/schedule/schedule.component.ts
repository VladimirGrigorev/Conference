import { Component, OnInit } from '@angular/core';
import { ScheduleService } from '../_services/schedule.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  lectures:Array<any> = [];
  constructor(private scheduleService:ScheduleService,
    private authService:AuthService) { }

  ngOnInit() {
    this.getLectures();
  }

  getLectures(){
    this.scheduleService.get()
      .subscribe((data:any)=>
        {this.lectures=data;
         
        });
  }

  deleteLecture(id,i){
    this.scheduleService.removeLecture(id)
      .subscribe(res=> {
        this.lectures.splice(i,1);
        this.authService.deleteListener(id)
      });
  }

  isEmpty(){
    return this.lectures.length==0;
  }

}
