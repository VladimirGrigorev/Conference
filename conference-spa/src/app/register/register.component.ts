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
  successFlag: boolean;
  successMessage: string;
  registerFlag = false;  
  constructor(private authService: AuthService,
    private router:Router) { }
  ngOnInit() {
  }
  register(){
    if(this.model['name'] && this.model['email'] && this.model['password'] && this.model['confirm']){
      if(this.model['password']===this.model['confirm']){
        this.authService.signup(this.model).subscribe(()=>
      {
          this.registerFlag = true;
          this.authService.signin(this.model);
          this.successFlag = true;
          this.successMessage = "Вы успешно зарегестрировались"
          setTimeout(() => {
            this.router.navigate(['/conferences']);
          }, 1500);
           },
          error => {
            console.log(error);
            this.errorMessage = error;
            this.errorFlag = true;
          }
      );
      }
      else{
        this.errorFlag = true;
        this.errorMessage = "Пароли не совпадают"
      }
    }
    else{
      this.errorFlag = true;
      this.errorMessage = "Не все поля заполнены"
    }
      
  }
  cancel() {
      console.log('cancelled');
  }
}
