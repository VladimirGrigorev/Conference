import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ConferencesComponent } from './conferences/conferences.component';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';
import { AddConferenceComponent } from './add-conference/add-conference.component';
import { LectureComponent } from './lecture/lecture.component';
import { RegisterComponent } from './register/register.component';
import { FilesComponent } from './files/files.component';
import { ScheduleComponent } from './schedule/schedule.component';
const routes: Routes = [
  { path: 'conferences', component: ConferencesComponent },
  { path: '', redirectTo:'conferences', pathMatch:'full'},
  { path: 'conferences/add-conf', component: AddConferenceComponent},
  { path: 'conferences/update/:id', component: AddConferenceComponent},
  { path: 'conferences/:id', component: ConferenceDetailComponent},
  { path: 'lectures/:id', component: LectureComponent},  
  { path: 'signup', component: RegisterComponent },
  { path: 'schedule', component: ScheduleComponent}]
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})


export class AppRoutingModule { }
