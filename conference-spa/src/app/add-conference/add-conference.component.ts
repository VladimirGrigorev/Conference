import { Component,  EventEmitter, OnInit, Input, Output } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { FormGroup, FormControl, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
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
  id:number;
  update : boolean;
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

  expertForm= this.fb.group({
    email:['']
  });

  constructor(
    private conferenceService:ConferencesService, 
    private router:Router,
    private route: ActivatedRoute,
    private fb:FormBuilder,
    private userService:UserService) { 
  }

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    if(this.id !=0)
      this.getConference();
    this.hideById('formSection');
  }

  fillForms(){
    this.conferenceForm.setValue({
      name: this.currentConference.name,
      info: this.currentConference.info,
      location: this.currentConference.location,
      dateTimeStartConference: this.currentConference.dateTimeStartConference,
      dateTimeFinishConference: this.currentConference.dateTimeFinishConference
    });
    //this.conferenceForm.value.name = this.currentConference.name;
  }

  getConference(){
    
    console.log(this.id);
    this.conferenceService.get(this.id)
      .subscribe(c =>
        { this.currentConference = c;
          this.listSections = this.currentConference.sections;
          this.listAdmins = this.currentConference.admins;
          this.fillForms();
          this.update = true;
          console.log(this.currentConference);
        });
    
  }

  addSection(){
    this.sectionForm.reset(); 
    this.blockById('formSection');
    this.hideById('addSection');
  }

  addLecture(i:number){
    this.lectureForm.reset(); 
    this.blockById('formLecture'+i);
    this.hideById('addLecture'+i);
  }

  onSubmit(){
  }

  saveSection(){   
    this.listSections.push(this.sectionForm.value);
    this.blockById('addSection');
    this.hideById('formSection');

  }

  close(){
    this.blockById('addSection');
    this.hideById('formSection');
  }

  closeLecture(i:number){
    this.blockById('addLecture'+i);
    this.hideById('formLecture'+i);
  }

  save(){
    
    this.currentConference = this.conferenceForm.value;
    this.currentConference.sections = this.listSections;
    this.currentConference.admins= this.listAdmins;
    console.log(this.currentConference);
    if(this.update){
      this.currentConference.id=this.id;
      this.conferenceService.update(this.currentConference)
        .subscribe(res=>
          this.router.navigate(['/conferences'])
        )
    }
    else{
      this.conferenceService.add(this.currentConference)
      .subscribe(res=>{
        this.router.navigate(['/conferences'])
      },
      error=>{
        this.errorMessageSave = error;
        this.errorFlagSave=true;
      });
    }
  }

  lectures(i:number){
    this.blockById('hide_'+i);
    this.blockById('compLectures_'+i);
    this.hideById('lectures_'+i);
  }

  saveLecture(i:number){
    if(this.listSections[i].lectures == undefined)
      this.listSections[i].lectures =[];
    this.listSections[i].lectures.push(this.lectureForm.value);
    this.blockById('addLecture'+i);
    this.hideById('formLecture'+i);
    this.lectureForm.reset(); 
  }

  hideLectures(i:number){
    this.blockById('lectures_'+i);
    this.hideById('hide_'+i);
    this.hideById('compLectures_'+i);
  }

  saveAdmin(){
    this.userService.getUserByEmail(this.adminForm.value.email)
      .subscribe((res:any)=>{
        this.listAdmins.push(res.body);
        this.blockById('addAdmin');
        this.hideById('adminForm');
      },
      error=>{
        this.errorMessage = error;
        this.errorFlag=true;
      });
  }

  closeAdmin(){
    this.blockById('addAdmin');
    this.hideById('adminForm');
  }

  addAdmin(){
    this.adminForm.reset(); 
    this.blockById('adminForm');
    this.hideById('addAdmin');
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

  addSpeaker(i,si){
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



  addExpert(i){
    this.expertForm.reset(); 
    this.blockById('formExpert'+i);
    this.hideById('addExpert'+i);
    this.blockById('hide_expert_'+i);
  }

  experts(i){
    this.hideById('expert_'+i);
    this.blockById('compExperts_'+i);
    this.blockById('hide_expert_'+i);
  }

  hideExperts(i){
    this.hideById('compExperts_'+i);
    this.blockById('expert_'+i);
    this.hideById('hide_expert_'+i);
  }

  saveExpert(i){
    if(this.listSections[i].experts == undefined)
      this.listSections[i].experts =[];

    this.userService.getUserByEmail(this.expertForm.value.email)
    .subscribe((res :any) =>{
      this.listSections[i].experts.push(res.body);
      this.blockById('addExpert'+i);
      this.hideById('formExpert'+i);
    },
    error=>{
      this.errorMessage = error;
      this.errorFlag=true;
    });
  }

  closeExpert(i){
    this.blockById('addExpert'+i);
    this.hideById('formExpert'+i);
  }

  deleteExpert(i,exi){
    this.listSections[i].experts.splice(exi,1);
  }
}
