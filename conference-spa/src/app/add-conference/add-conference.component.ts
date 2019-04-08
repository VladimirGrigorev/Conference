import { Component,  EventEmitter, OnInit, Input, Output } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { FormGroup, FormControl, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Section } from '../section';

@Component({
  selector: 'app-add-conference',
  templateUrl: './add-conference.component.html',
  styleUrls: ['./add-conference.component.css']
})
export class AddConferenceComponent implements OnInit {

  currentConference: any;
  listSections:Section[]=[];

  lectureForm=this.fb.group({
    topic:['', Validators.required],
    info:[''],
    dateTimeStart:['']
  });

  sectionForm=this.fb.group({
    name:['', Validators.required],
    info:[''],
  });

  conferenceForm= this.fb.group({
    name: ['', Validators.required],
    info: [''],
    location: [''],
    dateTimeStart: [''],
    dateTimeFinish: [''],
  });
  

  constructor(
    private conferenceService:ConferencesService, 
    private router:Router,
    private fb:FormBuilder) { 
  }

  ngOnInit() {
    document.getElementById('formSection').style.display='none';
  }

  addSection(){
    this.sectionForm.reset(); 
    document.getElementById('formSection').style.display='block';
    document.getElementById('addSection').style.display='none';
  }

  addLecture(i:number){
    this.lectureForm.reset(); 
    document.getElementById('addLecture'+i).style.display='none';
    document.getElementById('formLecture'+i).style.display='block';
  }

  onSubmit(){
  }

  saveSection(){   
    this.listSections.push(this.sectionForm.value);
    console.log(this.listSections);
    document.getElementById('formSection').style.display='none';
    document.getElementById('addSection').style.display='block';

  }

  close(){
    document.getElementById('formSection').style.display='none';
    document.getElementById('addSection').style.display='block';
  }

  closeLecture(i:number){
    console.log(i);
    document.getElementById('formLecture'+i).style.display='none';
    document.getElementById('addLecture'+i).style.display='block';
  }

  save(){
    this.currentConference = this.conferenceForm.value;
    this.currentConference.sections = this.listSections;
    console.log(this.currentConference);
    this.conferenceService.add(this.currentConference)
      .subscribe();
  }

  lectures(i:number){
    document.getElementById('lectures_'+i).style.display='none';
    document.getElementById('hide_'+i).style.display='block';
    document.getElementById('compLectures_'+i).style.display='block';
  }

  saveLecture(i:number){
    console.log(i);
    console.log(this.listSections);
    console.log(this.listSections[i].lectures);
    if(this.listSections[i].lectures == undefined)
      this.listSections[i].lectures =[];
    this.listSections[i].lectures.push(this.lectureForm.value);
    console.log(this.listSections);
    document.getElementById('formLecture'+i).style.display='none';
    document.getElementById('addLecture'+i).style.display='block';
    this.lectureForm.reset(); 
  }

  hideLectures(i:number){
    document.getElementById('lectures_'+i).style.display='block';
    document.getElementById('hide_'+i).style.display='none';
    document.getElementById('compLectures_'+i).style.display='none';
  }

}
