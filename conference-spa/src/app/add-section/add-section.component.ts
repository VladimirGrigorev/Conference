import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, FormArray } from '@angular/forms';
import { SectionsService } from '../_services/sections.service';

@Component({
  selector: 'app-add-section',
  templateUrl: './add-section.component.html',
  styleUrls: ['./add-section.component.css']
})
export class AddSectionComponent implements OnInit {

  opened:boolean;
  existing:boolean;

  openedLecture:boolean;

  listSections:any[] = [];

  sectionForm=this.fb.group({
    name: [''],
    info: ['']
  })
  constructor(
    private fb:FormBuilder,
    private sectionService: SectionsService) { }

  ngOnInit() {
    this.opened=false;
    this.existing=false;
    
  }

  save(){
    this.listSections.push(this.sectionForm.value);
    //this.sectionService.add(this.sectionForm.value);
    this.opened=false;
    this.existing=true;
  }

  addSection(){
    this.sectionForm.reset();
    this.opened = true;
  }

  onSubmit(){
  }

  edit(){
  }

  close(){
    this.opened=false;
  }

  hideLectures(){
    document.getElementById("compLectures").style.display="none";
    document.getElementById("hide").style.display="none";
    document.getElementById("edit").style.display="block";
    document.getElementById("lectures").style.display="block";

  }

  lectures(){
    
    this.openedLecture=true;
    document.getElementById("edit").style.display='none';
    document.getElementById("lectures").style.display='none';
    document.getElementById("hide").style.display='block';
    document.getElementById("compLectures").style.display='block';
  }
}
