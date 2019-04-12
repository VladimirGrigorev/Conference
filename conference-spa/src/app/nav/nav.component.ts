import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  nameUser:any;

  model: any = {};
  registerMode = false;
  regComponent  = true;
  constructor(private authService: AuthService,
    private userService:UserService) { }

  ngOnInit() {
  }

  login()
  {
    this.authService.signin(this.model).subscribe(next => {
        console.log('success');
        this.regComponent = !this.regComponent;
        this.nameUser = this.userService.getName().subscribe();
        console.log(this.nameUser);
            
    }, error => {
      console.log(error);
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
      localStorage.removeItem('token');
      console.log('Logged out');
      this.registerMode = !this.registerMode;

  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

}