import { Component, OnInit } from '@angular/core';
import { InfoPage } from '../info-page';
import { InfoPageService } from '../_services/info-page.service';
import { ActivatedRoute, Router } from '@angular/router';
import {Location} from '@angular/common';
import { switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-add-info-page',
  templateUrl: './add-info-page.component.html',
  styleUrls: ['./add-info-page.component.css']
})
export class AddInfoPageComponent implements OnInit {

  page : InfoPage;
  conferenceId: number;
  ckeConfig: any;
  public Editor = ClassicEditor;

  constructor(
    private infoPageService: InfoPageService,
    private route: ActivatedRoute,
    private location: Location,
    private router: Router ) { 

      this.page = new InfoPage();
      this.page.data = "";//"<script>alert(\"hola\")</script>";//"<p><img src=\"http://google.com/\" onerror=\"alert('Sanatizing not working :(')\">Incorrect image src should not open alert</p>";
    }

  ngOnInit() {    
    this.ckeConfig = {
      allowedContent: false,
      //extraPlugins: 'divarea',
      height: 1000,
      forcePasteAsPlainText: true,
      
      //removeButtons = 'MediaEmbed, Image';
      toolbar: [
        'heading', '|', "bold", "italic","link",'bulletedList', 'numberedList', 'alignment', 'undo', 'redo'
      ],
      language:"ru"
    };

    this.get();    
  }

  get() : void {
    if(this.route.snapshot.paramMap.has('id'))
    {
      this.route.paramMap.pipe(
        switchMap(params => {
          const id = +params.get('id');
          return this.infoPageService.get(id);
        })
      ).subscribe(p => this.page = p) ;

      // const id = +this.route.snapshot.paramMap.get('id');
      // this.applicationService.get(id)
      //   .subscribe(art => this.app = art);
    }
    else if(this.route.snapshot.paramMap.has('confId'))
    {
      this.route.paramMap.pipe(
        switchMap(params => {
          const id = +params.get('confId');
          return of(id); //this.infoPageService.get(id);
        })
      ).subscribe(id=>this.conferenceId = id) ;

      // const id = +this.route.snapshot.paramMap.get('id');
      // this.applicationService.get(id)
      //   .subscribe(art => this.app = art);
    }
  }

  onSubmit() : void{
    if (this.page.id == null) { 
      this.infoPageService.add(this.conferenceId, this.page)
      .subscribe(id => {
          this.page.id = +id;
          this.goToApp();
        });
      
    } else {
      //todo update
      //  this.applicationService.update(this.app)
      //  .subscribe(() => this.goToApp());
    }  	
    
  }

  goBack(): void {
    this.location.back();
  }

  goToApp(): void{
    
    this.router.navigate([`/conferences/${this.conferenceId}/pages/${this.page.id}`]);
  }

}
