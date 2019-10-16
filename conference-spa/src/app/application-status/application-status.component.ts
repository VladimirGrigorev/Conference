import { Component, OnInit, Input } from '@angular/core';
import { ApplicationStatus } from '../application';

@Component({
  selector: 'app-application-status',
  templateUrl: './application-status.component.html',
  styleUrls: ['./application-status.component.css']
})
export class ApplicationStatusComponent implements OnInit {

  @Input()
  appStatus:ApplicationStatus;

  applicationStatus = ApplicationStatus;

  constructor() { 
    this.applicationStatus = ApplicationStatus;
  }

  ngOnInit() {
  }

}
