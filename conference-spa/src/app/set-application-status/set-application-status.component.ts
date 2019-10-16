import { Component, OnInit, Input } from '@angular/core';
import { ApplicationStatus, ApplicationStatusInfo } from '../application';

@Component({
  selector: 'app-set-application-status',
  templateUrl: './set-application-status.component.html',
  styleUrls: ['./set-application-status.component.css']
})
export class SetApplicationStatusComponent implements OnInit {

  @Input()
  app: ApplicationStatusInfo;

  statuses: StatusDescriptor[] = [
    {name : "Принято", status : ApplicationStatus.Accepted},
    {name : "Отклонено из-за оформления", status : ApplicationStatus.RejectedDesign},
    {name : "Отклонено из-за содержания", status : ApplicationStatus.RejectedContent},
    {name : "На рассмотрении", status : ApplicationStatus.Pending}
  ]

  constructor() { 
  }

  ngOnInit() {
  }

}

export class StatusDescriptor{
  name: string;
  status: ApplicationStatus;
}