import { Component, OnInit } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-conferences',
  templateUrl: './conferences.component.html',
  styleUrls: ['./conferences.component.css']
})
export class ConferencesComponent implements OnInit {

  conferences: Array<any>;
  message: string;

  constructor(private conferencesService: ConferencesService,
    private authService: AuthService) { 
    
  }

  ngOnInit() {
    this.getConferences();
  }

  getConferences(): void{
    this.conferencesService.getAll()
      .subscribe((conferences :any ) => this.conferences = conferences);
  }

  deleteConference(id:number){
    this.conferencesService.deleteById(id).subscribe(
      data => {
        this.message = "Вы успешно удалили конференцию";
      }
    );
    setTimeout(() => {
      this.ngOnInit();
      this.message = '';
    }, 1000);

  }

  isAddDisplayed():boolean{
    return this.authService.isAuthenticated()&&this.authService.isAdmin();
  }

  isEditDisplayed(id:number):boolean{
    return this.authService.isConfAdmin(id);
  }
}
