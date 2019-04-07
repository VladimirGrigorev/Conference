import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ConferencesComponent } from './conferences/conferences.component';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';
import { LectureComponent } from './lecture/lecture.component';

const routes: Routes = [
  { path: 'conference', component: ConferencesComponent },
  { path: '', redirectTo:'conference', pathMatch:'full'},
  { path: 'conference/:id', component: ConferenceDetailComponent},
  { path: 'lectures/:id', component: LectureComponent}
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})


export class AppRoutingModule { }
