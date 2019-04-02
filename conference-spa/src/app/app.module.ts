import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HttpClient, HttpClientModule} from 'node_modules/@angular/common/http';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
