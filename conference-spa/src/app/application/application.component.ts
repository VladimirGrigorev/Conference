import { Component, OnInit } from '@angular/core';
import { Application } from '../application';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import {Location} from '@angular/common';
import { ScheduleService } from '../_services/schedule.service';
import { AuthService } from '../_services/auth.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { ModalFileUploadComponent } from '../modal-file-upload/modal-file-upload.component';
import { ApplicationService } from '../_services/application.service';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css']
})
export class ApplicationComponent implements OnInit {

  private id:number;
  private app: Application;

  constructor(private applicationService: ApplicationService,
    private route: ActivatedRoute,
    private location: Location,
    private scheduleService: ScheduleService,
    private authService: AuthService) { }

  ngOnInit() {
    this.get();
    this.app = new Application();
  }

  get(){
    this.route.paramMap.pipe(
      switchMap(params => {
        this.id = +params.get('id');
        return this.applicationService.get(this.id);
      })
    ).subscribe(ap=> this.app = ap) ;    
  }

  goBack(){
    this.location.back();
  }

  isAppAuthor(){
    return this.authService.isAuthor(this.app.userId);
  }
}
