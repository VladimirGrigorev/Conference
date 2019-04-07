import { Component,  EventEmitter, OnInit, Input, Output } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { FormGroup, FormControl, FormBuilder, FormArray } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-conference',
  templateUrl: './add-conference.component.html',
  styleUrls: ['./add-conference.component.css']
})
export class AddConferenceComponent implements OnInit {

  conferenceForm= this.fb.group({
    name: [''],
    info: [''],
    location: [''],
    dateTimeStart: [''],
    dateTimeFinish: ['']
  });

  @Input() currentConference: any;

  constructor(
    private conferenceService:ConferencesService, 
    private router:Router,
    private fb:FormBuilder) { 
  }

  ngOnInit() {
  }

  onSubmit(){

  }

  save(){
    this.conferenceService.add(this.conferenceForm.value);
  }

}
