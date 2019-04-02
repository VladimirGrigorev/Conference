import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HttpClient, HttpClientModule} from 'node_modules/@angular/common/http';
import { ConferencesComponent } from './conferences/conferences.component';
import { AppRoutingModule } from './/app-routing.module';
import { ConferenceDetailComponent } from './conference-detail/conference-detail.component';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      ConferencesComponent,
      ConferenceDetailComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      AppRoutingModule,
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
