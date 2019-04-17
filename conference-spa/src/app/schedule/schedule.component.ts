import { Component, OnInit } from '@angular/core';
import { ScheduleService } from '../_services/schedule.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  lectures:Array<any> = [];
  constructor(private scheduleService:ScheduleService) { }

  ngOnInit() {
    this.getLectures();
  }

  getLectures(){
    this.scheduleService.get()
      .subscribe((data:any)=>
        {this.lectures=data;
         
        });
  }

}
