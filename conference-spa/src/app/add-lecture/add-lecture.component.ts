import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-add-lecture',
  templateUrl: './add-lecture.component.html',
  styleUrls: ['./add-lecture.component.css']
})
export class AddLectureComponent implements OnInit {

  opened: boolean;
  existing:boolean;

  lectureForm=this.fb.group({
    topic:[''],
    info:[''],
    dateTimeStart:['']
  })

  listLectures:any[]=[];

  constructor(private fb:FormBuilder) { }

  ngOnInit() {
    this.opened=false;
    this.existing=false;
  }

  save(){
    this.listLectures.push(this.lectureForm.value);
    //this.sectionService.add(this.sectionForm.value);
    this.opened=false;
    this.existing=true;
  }

  addLecture(){
    this.lectureForm.reset();
    this.opened = true;
  }

  onSubmit(){
  }

  edit(){    
  }

  close(){
    this.opened=false;
  }

}
