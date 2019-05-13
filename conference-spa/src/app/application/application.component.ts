import { Component, OnInit } from '@angular/core';
import { Application, ApplicationStatusInfo } from '../application';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import {Location} from '@angular/common';
import { ScheduleService } from '../_services/schedule.service';
import { AuthService } from '../_services/auth.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { ModalFileUploadComponent } from '../modal-file-upload/modal-file-upload.component';
import { ApplicationService } from '../_services/application.service';
import { ModalSetApplicationStatusComponent } from '../modal-set-application-status/modal-set-application-status.component';

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
    private authService: AuthService,
    private modalService: NgbModal) { }

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

  openModalSetStatus() {
    const modalRef = this.modalService.open(ModalSetApplicationStatusComponent);
    let app = new ApplicationStatusInfo();
    app.applicationStatus = this.app.applicationStatus;    
    modalRef.componentInstance.app = app;

    modalRef.result.then(() => {
        this.applicationService.setStatus(this.app.id, app)
        .subscribe(()=>this.app.applicationStatus = app.applicationStatus)
      }, () => {
        alert(app.applicationStatus);
        //alert("Dismissed");
      });    
  }
}
