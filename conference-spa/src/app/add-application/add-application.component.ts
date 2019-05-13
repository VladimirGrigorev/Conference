import { Component, OnInit } from '@angular/core';
import { Application } from '../application';
import { ApplicationService } from '../_services/application.service';
import { ConferencesService } from '../_services/conferences.service';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Conference } from '../conference';
import {Location} from '@angular/common';

@Component({
  selector: 'app-add-application',
  templateUrl: './add-application.component.html',
  styleUrls: ['./add-application.component.css']
})
export class AddApplicationComponent implements OnInit {

  app : Application;
  conferences: Conference[];

  constructor(
    private applicationService: ApplicationService,
    private conferencesService: ConferencesService,
    private route: ActivatedRoute,
    private location: Location,
    private router: Router ) { 

      this.app = new Application();
    }

  ngOnInit() {    
    this.getConferencesWithSections();
    this.getArticle();    
  }

  getArticle() : void {
    if(this.route.snapshot.paramMap.has('id'))
    {
      this.route.paramMap.pipe(
        switchMap(params => {
          const id = +params.get('id');
          return this.applicationService.get(id);
        })
      ).subscribe(art => this.app = art) ;

      // const id = +this.route.snapshot.paramMap.get('id');
      // this.applicationService.get(id)
      //   .subscribe(art => this.app = art);
    }
  }

  getConferencesWithSections(){
    this.conferencesService.getAllWithSections()
      .subscribe(cs=>this.conferences = cs);
  }

  onSubmit() : void{
    if (this.app.id == null) { 
      this.applicationService.add(this.app)
      .subscribe(id => {
          this.app.id = +id;
          this.goToApp();
        });
      
    } else {
      //todo update
      //  this.applicationService.update(this.app)
      //  .subscribe(() => this.goToApp());
    }  	
    
  }

  goBack(): void {
    this.location.back();
  }

  goToApp(): void{
    this.router.navigate([`/applications/${this.app.id}`]);
  }
}
