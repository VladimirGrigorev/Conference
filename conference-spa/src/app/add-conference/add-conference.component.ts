import { Component,  EventEmitter, OnInit, Input, Output } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { FormGroup, FormControl, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Section } from '../section';
import { UserService } from '../_services/user.service';
import { error } from '@angular/compiler/src/util';
import { UserInfo } from '../userInfo';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-conference',
  templateUrl: './add-conference.component.html',
  styleUrls: ['./add-conference.component.css']
})
export class AddConferenceComponent implements OnInit {

  currentConference: any;
  listSections:Section[]=[];
  listAdmins:any[] = [];

  errorMessage:string;
  errorFlag:boolean;
  
  errorMessageSave:string;
  errorFlagSave:boolean;

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
    dateTimeStartConference: [''],
    dateTimeFinishConference: [''],
  });
  
  adminForm= this.fb.group({
    email:['']
  });

  speakerForm= this.fb.group({
    email:['']
  });

  constructor(
    private conferenceService:ConferencesService, 
    private router:Router,
    private fb:FormBuilder,
    private userService:UserService) { 
  }

  ngOnInit() {
    this.hideById('formSection');
    // document.getElementById('formSection').style.display='none';
  }

  addSection(){
    this.sectionForm.reset(); 
    this.blockById('formSection');
    this.hideById('addSection');
    // document.getElementById('formSection').style.display='block';
    // document.getElementById('addSection').style.display='none';
  }

  addLecture(i:number){
    this.lectureForm.reset(); 
    this.blockById('formLecture'+i);
    this.hideById('addLecture'+i);
    // document.getElementById('addLecture'+i).style.display='none';
    // document.getElementById('formLecture'+i).style.display='block';
  }

  onSubmit(){
  }

  saveSection(){   
    this.listSections.push(this.sectionForm.value);
    this.blockById('addSection');
    this.hideById('formSection');
    // document.getElementById('formSection').style.display='none';
    // document.getElementById('addSection').style.display='block';

  }

  close(){
    this.blockById('addSection');
    this.hideById('formSection');
    // document.getElementById('formSection').style.display='none';
    // document.getElementById('addSection').style.display='block';
  }

  closeLecture(i:number){
    this.blockById('addLecture'+i);
    this.hideById('formLecture'+i);
    // document.getElementById('formLecture'+i).style.display='none';
    // document.getElementById('addLecture'+i).style.display='block';
  }

  save(){
    this.currentConference = this.conferenceForm.value;
    this.currentConference.sections = this.listSections;
    console.log(this.currentConference);
    this.conferenceService.add(this.currentConference)
      .subscribe(res=>{
        this.router.navigate(['/conferences'])
      },
      error=>{
        this.errorMessageSave = error;
        this.errorFlagSave=true;
      });
  }

  lectures(i:number){
    this.blockById('hide_'+i);
    this.blockById('compLectures_'+i);
    this.hideById('lectures_'+i);
    // document.getElementById('lectures_'+i).style.display='none';
    // document.getElementById('hide_'+i).style.display='block';
    // document.getElementById('compLectures_'+i).style.display='block';
  }

  saveLecture(i:number){
    if(this.listSections[i].lectures == undefined)
      this.listSections[i].lectures =[];
    this.listSections[i].lectures.push(this.lectureForm.value);
    this.blockById('addLecture'+i);
    this.hideById('formLecture'+i);
    // document.getElementById('formLecture'+i).style.display='none';
    // document.getElementById('addLecture'+i).style.display='block';
    this.lectureForm.reset(); 
  }

  hideLectures(i:number){
    this.blockById('lectures_'+i);
    this.hideById('hide_'+i);
    this.hideById('compLectures_'+i);
    // document.getElementById('lectures_'+i).style.display='block';
    // document.getElementById('hide_'+i).style.display='none';
    // document.getElementById('compLectures_'+i).style.display='none';
  }

  saveAdmin(){
    this.userService.getUserByEmail(this.adminForm.value.email)
      .subscribe(res=>{
        this.listAdmins.push(this.adminForm.value.email);
        this.blockById('addAdmin');
        this.hideById('adminForm');
      },
      error=>{
        this.errorMessage = error;
        this.errorFlag=true;
      });
    
   
    // document.getElementById('adminForm').style.display='none';
    // document.getElementById('addAdmin').style.display='block';
  }

  closeAdmin(){
    this.blockById('addAdmin');
    this.hideById('adminForm');
    // document.getElementById('adminForm').style.display='none';
    // document.getElementById('addAdmin').style.display='block';
  }

  addAdmin(){
    this.adminForm.reset(); 
    this.blockById('adminForm');
    this.hideById('addAdmin');
    //document.getElementById('adminForm').style.display='block';
    //document.getElementById('addAdmin').style.display='none';
  }

  deleteAdmin(j){
    this.listAdmins.splice(j,1);
  }

  hideById(id){
    document.getElementById(id).style.display='none';
  }

  blockById(id){
    document.getElementById(id).style.display='block';
  }

  deleteSection(i){
    this.listSections.splice(i,1);
  }

  deleteLecture(i,si){
    this.listSections[i].lectures.splice(si,1);
  }

  addSpeaker(i,si){//i,idLecture
    //this.listSections[i].lectures[idLecture].speakers.push();
    this.speakerForm.reset(); 
    this.blockById('formSpeaker'+i+'_'+si);
    this.hideById('addSpeaker'+i+'_'+si);
    this.blockById('hide_speaker_'+i+'_'+si);
  }

  speakers(i,si){
    this.hideById('speaker_'+i+'_'+si);
    this.blockById('compSpeakers_'+i+'_'+si);
    this.blockById('hide_speaker_'+i+'_'+si);
  }

  hideSpeakers(i,si){
    this.hideById('compSpeakers_'+i+'_'+si);
    this.blockById('speaker_'+i+'_'+si);
    this.hideById('hide_speaker_'+i+'_'+si);
    // this.hideById('hide_speaker_'+i);
    // this.blockById('speaker_'+i);
  }

  saveSpeaker(i,si){
    if(this.listSections[i].lectures[si].speakers == undefined)
      this.listSections[i].lectures[si].speakers =[];

    this.userService.getUserByEmail(this.speakerForm.value.email)
    .subscribe((res :any) =>{
      this.listSections[i].lectures[si].speakers.push(res.body);
      this.blockById('addSpeaker'+i+'_'+si);
      this.hideById('formSpeaker'+i+'_'+si);
    },
    error=>{
      this.errorMessage = error;
      this.errorFlag=true;
    });
  }

  closeSpeaker(i,si){
    this.blockById('addSpeaker'+i+'_'+si);
    this.hideById('formSpeaker'+i+'_'+si);
  }

  deleteSpeaker(i,si,spi){
    this.listSections[i].lectures[si].speakers.splice(spi,1);
  }
}
