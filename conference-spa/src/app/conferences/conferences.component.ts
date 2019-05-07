import { Component, OnInit } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-conferences',
  templateUrl: './conferences.component.html',
  styleUrls: ['./conferences.component.css']
})
export class ConferencesComponent implements OnInit {

  conferences: Array<any>;

  constructor(private conferencesService: ConferencesService, private authService: AuthService) { 
    
  }

  ngOnInit() {
    this.getConferences();
  }

  getConferences(): void{
    this.conferencesService.getAll()
      .subscribe((conferences :any ) => this.conferences = conferences);
  }

  isAddDisplayed():boolean{
    return this.authService.isAuthenticated()&&this.authService.isAdmin();
  }

  isDisplayed(id:number):boolean{
    return id==8;
  }
}
