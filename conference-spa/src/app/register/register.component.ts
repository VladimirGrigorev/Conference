import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { registerContentQuery } from '@angular/core/src/render3/instructions';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  errorMessage: string;
  errorFlag: boolean;
  registerFlag = false;  constructor(private authService: AuthService,
    private router:Router) { }
  ngOnInit() {
  }
  register(){
      this.authService.signup(this.model).subscribe(()=>
      {
          this.registerFlag = true;
          this.authService.signin(this.model);
          this.router.navigate(['/conferences']);      },
          error => {
            console.log(error);
            this.errorMessage = error;
            this.errorFlag = true;
          }
      );
  }
  cancel() {
      console.log('cancelled');
  }
}
