import { Component, OnInit } from '@angular/core';
import { InfoPage } from '../info-page';
import { InfoPageService } from '../_services/info-page.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-info-page',
  templateUrl: './info-page.component.html',
  styleUrls: ['./info-page.component.css']
})
export class InfoPageComponent implements OnInit {

  private id:number;
  page = new InfoPage();

  constructor(private infoPageService: InfoPageService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.getInfoPage();
    
  }

  getInfoPage(){
    this.route.paramMap.pipe(
      switchMap(params => {
        this.id = +params.get('pid');
        return this.infoPageService.get(this.id);
      })
    ).subscribe(p=> this.page = p) ;    
  }
}
