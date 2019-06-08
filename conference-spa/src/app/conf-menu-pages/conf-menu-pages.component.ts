import { Component, OnInit } from '@angular/core';
import { InfoPageService } from '../_services/info-page.service';
import { InfoPage } from '../info-page';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-conf-menu-pages',
  templateUrl: './conf-menu-pages.component.html',
  styleUrls: ['./conf-menu-pages.component.css']
})
export class ConfMenuPagesComponent implements OnInit {

  private confId:number;
  pages: InfoPage[];

  constructor(private infoPageService: InfoPageService,
    private authService: AuthService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.getInfoPages();
    
  }

  getInfoPages(){
    this.route.paramMap.pipe(
      switchMap(params => {
        this.confId = +params.get('id');
        return this.infoPageService.getAll(this.confId);
      })
    ).subscribe(ps=> this.pages = ps) ;    
  }

  isConfAdmin():boolean{
    return this.authService.isConfAdmin(this.confId);
  }
}
