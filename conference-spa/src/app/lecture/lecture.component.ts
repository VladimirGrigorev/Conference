import { Component, OnInit } from '@angular/core';
import { Lecture } from '../lecture';
import { LecturesService } from '../_services/lectures.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import { p } from '@angular/core/src/render3';
import {Location} from '@angular/common';
import { ScheduleService } from '../_services/schedule.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrls: ['./lecture.component.css']
})
export class LectureComponent implements OnInit {

  private id:number;
  lecture = new Lecture();
  private isAppend:boolean = false;

  constructor(private lecturesService: LecturesService,
    private route: ActivatedRoute,
    private location: Location,
    private scheduleService: ScheduleService,
    private authService:AuthService) { }

  ngOnInit() {
    this.getLecture();
    
  }

  getLecture(){
    this.route.paramMap.pipe(
      switchMap(params => {
        this.id = +params.get('id');
        return this.lecturesService.get(this.id);
      })
    ).subscribe(lec=> this.lecture = lec) ;    
  }

  goBack(){
    this.location.back();
  }

  addSchedule(){
    this.isAppend = true;
    this.scheduleService.addLecture(this.id)
      .subscribe(res => 
        {
          this.authService.addListener(this.id);
        });
  }

  removeSchedule(){
    this.scheduleService.removeLecture(this.id)
      .subscribe(res=>
         this.authService.deleteListener(this.id));
  }

  isListener(){
    return this.authService.isListener(this.id);
  }
}
