import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConferencesService } from '../_services/conferences.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Section } from '../section';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-change-conference',
  templateUrl: './change-conference.component.html',
  styleUrls: ['./change-conference.component.css']
})
export class ChangeConferenceComponent implements OnInit {

  conference;
  currentConference: any;
  listSections:Section[]=[];
  listAdmins:any[] = [];
  errorMessage:string;
  errorFlag:boolean;
  start = new Date();
  end = new Date();
  update
  dateerror=''
  id;
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

  constructor(private route:ActivatedRoute,
    private confService: ConferencesService,
    private fb:FormBuilder,
    private conferenceService: ConferencesService,
    private router: Router,
    private userService: UserService) { }

  ngOnInit() {
    this.id = +(this.route.snapshot.paramMap.get('id') || this.route.snapshot.parent.paramMap.get('id'));
    this.getConference();
    this.hideById('formSection');
    
  }

  getConference(): void{
    
    this.confService.get(this.id)
      .subscribe(conference => {
        this.conference = conference;

        console.log(this.conference);
        this.start = new Date(this.conference.dateTimeStartConference)
        this.end = new Date(this.conference.dateTimeFinishConference)
        this.listAdmins = this.conference.admins;
        this.listSections = this.conference.sections;
      } );
     
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
    if(new Date(this.start).getTime()<new Date(this.end).getTime()){
      this.dateerror = ''
      this.currentConference = this.conferenceForm.value;
      this.currentConference.sections = this.listSections;
      this.currentConference.admins= this.listAdmins;
        this.currentConference.id=this.id;
        this.conferenceService.update(this.currentConference)
          .subscribe(res=>
            this.router.navigate(['/conferences'])
          )
    }
    else{
      this.dateerror='Введите даты корректно'
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
        this.errorMessage = error.body;
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
      this.errorMessage = error.body;
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
      this.errorMessage = error.body;
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
