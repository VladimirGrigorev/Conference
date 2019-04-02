import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ConferencesComponent } from './conferences/conferences.component';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';

const routes: Routes = [
  { path: 'conference', component: ConferencesComponent },
  { path: '', redirectTo:'conference', pathMatch:'full'},
  { path: 'conference/:id', component: ConferenceDetailComponent}]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})


export class AppRoutingModule { }
