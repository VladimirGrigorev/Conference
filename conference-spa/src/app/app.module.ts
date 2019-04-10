import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS} from 'node_modules/@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import {ErrorInterceptorProvider } from './_services/error.interceptor';
import {TokenHttpRequestInterceptor } from './_services/token-http-request.interceptor';


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegisterComponent,
      HomeComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      {
         provide: HTTP_INTERCEPTORS,
         useClass: TokenHttpRequestInterceptor,
         multi: true
       }
      
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
