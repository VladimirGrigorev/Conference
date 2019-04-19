import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { getTypeNameForDebugging } from '@angular/core/src/change_detection/differs/iterable_differs';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  nameUser:any = {};

  model: any = {};
  registerMode = false;
  regComponent  = true;
  constructor(private authService: AuthService,
    private userService:UserService) { }

  ngOnInit() {
    this.getName();
  }

  login()
  {
    this.authService.signin(this.model).subscribe(next => {
        console.log('success');
        this.regComponent = !this.regComponent;
        this.getName();    
        this.model.email = "";
        this.model.passhash = ""; 
            
    }, error => {
      console.log(error); 
      localStorage.clear();
    }
    );
  }
  loggedIn()
  {
    const token = localStorage.getItem('token');
    return !!token;
  }
  logout()
  {
    this.authService.logout();
    // .subscribe(next => {
    //   console.log('success logout');
    //   this.registerMode = !this.registerMode;
    //   }, error => {
    //    console.log(error);
    //   }
    //);

  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getName(){
    this.userService.getName()
      .subscribe((data: any) => this.nameUser = data);
  }

}