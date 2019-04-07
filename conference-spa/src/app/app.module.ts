import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HttpClient, HttpClientModule} from 'node_modules/@angular/common/http';
import { ConferencesComponent } from './conferences/conferences.component';
import { AppRoutingModule } from './/app-routing.module';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';
import { AddConferenceComponent } from './add-conference/add-conference.component';
import { AddSectionComponent } from './add-section/add-section.component';
import { AddLectureComponent } from './add-lecture/add-lecture.component';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      ConferencesComponent,
      ConferenceDetailComponent,
      AddConferenceComponent,
      AddSectionComponent,
      AddLectureComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      AppRoutingModule,
      ReactiveFormsModule,
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
