import { Component, OnInit } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';

@Component({
  selector: 'app-conferences',
  templateUrl: './conferences.component.html',
  styleUrls: ['./conferences.component.css']
})
export class ConferencesComponent implements OnInit {

  conferences: Array<any>;

  constructor(private conferencesService: ConferencesService) { 
    this.getConferences();
  }

  ngOnInit() {
  }

  getConferences(): void{
    this.conferencesService.getAll()
      .subscribe((conferences :any ) => this.conferences = conferences);
  }

  isDisplayed(id:number):boolean{
    return id===1;
  }
}
