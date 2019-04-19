import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class TokenHttpRequestInterceptor implements HttpInterceptor{

    constructor(public auth: AuthService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if(this.auth.isAuthenticated()){
            const cloneReq = req.clone({
                headers: req.headers.set("Authorization", this.auth.getAuth())
            });
            return next.handle(cloneReq);            
        }
        return next.handle(req);
    }
}
