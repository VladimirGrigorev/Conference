import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  errorMessage: string;
  errorFlag: boolean;
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  register(){
      this.authService.signup(this.model).subscribe(()=>
      {
          this.authService.signin(this.model);
      },
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
