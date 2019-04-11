import { Component, OnInit } from '@angular/core';
import { Lecture } from '../lecture';
import { LecturesService } from '../_services/lectures.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import { p } from '@angular/core/src/render3';
import {Location} from '@angular/common';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrls: ['./lecture.component.css']
})
export class LectureComponent implements OnInit {

  private id:number;
  lecture = new Lecture();

  constructor(private lecturesService: LecturesService,
    private route: ActivatedRoute,
    private location: Location) { }

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
}
