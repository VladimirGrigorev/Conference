import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../_services/application.service';
import { Application, ApplicationStatus } from '../application';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.css']
})
export class ApplicationsComponent implements OnInit {

  applications: Application[];
  isMyApps = false;
  applicationStatus = ApplicationStatus;


  sub:any;

  constructor(private applicationService: ApplicationService,
    private route: ActivatedRoute) {
      this.applicationStatus = ApplicationStatus;
    }

  ngOnInit() {
    this.sub = this.route
      .data
      .subscribe(v => this.isMyApps = v.isMy);

    this.get();
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  get(){
    if(this.isMyApps)
      this.applicationService.getMy().subscribe(e=>this.applications = e);
    else
      this.applicationService.getConsidered().subscribe(e=>this.applications = e);
  } 

}
