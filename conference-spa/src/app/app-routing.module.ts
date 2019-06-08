import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ConferencesComponent } from './conferences/conferences.component';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';
import { AddConferenceComponent } from './add-conference/add-conference.component';
import { LectureComponent } from './lecture/lecture.component';
import { RegisterComponent } from './register/register.component';
import { FilesComponent } from './files/files.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ApplicationsComponent } from './applications/applications.component';
import { ApplicationComponent } from './application/application.component';
import { AddApplicationComponent } from './add-application/add-application.component';
import { AddInfoPageComponent } from './add-info-page/add-info-page.component';
import { InfoPageComponent } from './info-page/info-page.component';
import { ConfInfoPagesComponent } from './conf-info-pages/conf-info-pages.component';

const routes: Routes = [
  { path: 'conferences', component: ConferencesComponent },
  { path: '', redirectTo:'conferences', pathMatch:'full'},
  { path: 'conferences/add-conf', component: AddConferenceComponent},
  { path: 'conferences/update/:id', component: AddConferenceComponent},
  { path: 'conferences/:confId/pages/add', component: AddInfoPageComponent},
  { path: 'conferences/:id', component: ConfInfoPagesComponent,
    children:[
      {path: '', component: ConferenceDetailComponent},
      {path: 'pages/:pid', component: InfoPageComponent}
    ]
  },  
  { path: 'lectures/:id', component: LectureComponent},  
  { path: 'applications/add', component: AddApplicationComponent},
  { path: 'applications/:id', component: ApplicationComponent},
  { path: 'signup', component: RegisterComponent },
  { path: 'schedule', component: ScheduleComponent},
  { path: 'apps/my', component: ApplicationsComponent, data:{isMy : true}},
  { path: 'apps/considered', component: ApplicationsComponent, data:{isMy : false}}
]
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})


export class AppRoutingModule { }
