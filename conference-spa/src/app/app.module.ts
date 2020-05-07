import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS} from 'node_modules/@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import {ErrorInterceptorProvider } from './_services/error.interceptor';
import {TokenHttpRequestInterceptor } from './_services/token-http-request.interceptor';
import { ConferencesComponent } from './conferences/conferences.component';
import { AppRoutingModule } from './/app-routing.module';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';
import { AddConferenceComponent } from './add-conference/add-conference.component';
import { AddSectionComponent } from './add-section/add-section.component';
import { AddLectureComponent } from './add-lecture/add-lecture.component';
import { LectureComponent } from './lecture/lecture.component';
import { ForumComponent } from './forum/forum.component';
import { FilesComponent } from './files/files.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { ModalComponent } from './modal/modal.component';
import { ModalFileUploadComponent } from './modal-file-upload/modal-file-upload.component';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ApplicationsComponent } from './applications/applications.component';
import { ApplicationComponent } from './application/application.component';
import { ApplicationStatusComponent } from './application-status/application-status.component';
import { AddApplicationComponent } from './add-application/add-application.component';
import { SetApplicationStatusComponent } from './set-application-status/set-application-status.component';
import { ModalSetApplicationStatusComponent } from './modal-set-application-status/modal-set-application-status.component';
import { AddInfoPageComponent } from './add-info-page/add-info-page.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { InfoPageComponent } from './info-page/info-page.component';
import { ConfMenuPagesComponent } from './conf-menu-pages/conf-menu-pages.component';
import { ConfInfoPagesComponent } from './conf-info-pages/conf-info-pages.component';
import { ChangeConferenceComponent } from './change-conference/change-conference.component';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegisterComponent,
      ConferencesComponent,
      ConferenceDetailComponent,
      AddConferenceComponent,
      AddSectionComponent,
      AddLectureComponent,
      LectureComponent,
      ForumComponent,
      FilesComponent,
      ScheduleComponent,
      FileUploadComponent,
      ModalComponent,
      ModalFileUploadComponent,
      ApplicationsComponent,
      ApplicationComponent,
      ApplicationStatusComponent,
      AddApplicationComponent,
      SetApplicationStatusComponent,
      ModalSetApplicationStatusComponent,
      AddInfoPageComponent,
      InfoPageComponent,
      ConfMenuPagesComponent,
      ConfInfoPagesComponent,
      ChangeConferenceComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      AppRoutingModule,
      CKEditorModule,
      ReactiveFormsModule,
      NgbModule.forRoot()
   ],
   providers: [
      AuthService,
      //ErrorInterceptorProvider,
      {
         provide: HTTP_INTERCEPTORS,
         useClass: TokenHttpRequestInterceptor,
         multi: true
       },
       NgbActiveModal
      
   ],
   bootstrap: [
      AppComponent
   ],
   entryComponents: [
      ModalFileUploadComponent,
      ModalSetApplicationStatusComponent
    ]
})
export class AppModule { }
