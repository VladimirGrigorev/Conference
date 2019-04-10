import { Component, OnInit } from '@angular/core';
import { Lecture } from '../lecture';
import { LecturesService } from '../_services/lectures.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import { p } from '@angular/core/src/render3';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-lecture',
  templateUrl: './lecture.component.html',
  styleUrls: ['./lecture.component.css']
})
export class LectureComponent implements OnInit {

  lecture = new Lecture();

  constructor(private lecturesService: LecturesService,
    private route: ActivatedRoute,
    private messageService:MessageService) { }

  ngOnInit() {
    this.getLecture();    
  }

  getLecture(){
    this.route.paramMap.pipe(
      switchMap(params => {
        let id = +params.get('id');
        this.messageService.addIDLecture(id);
        return this.lecturesService.get(id);
      })
    ).subscribe(lec=> this.lecture = lec) ;    
  }

}
