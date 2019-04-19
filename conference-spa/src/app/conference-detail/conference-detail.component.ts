import { Component, OnInit } from '@angular/core';
import { ConferencesService } from '../_services/conferences.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-conference-detail',
  templateUrl: './conference-detail.component.html',
  styleUrls: ['./conference-detail.component.css']
})
export class ConferenceDetailComponent implements OnInit {

  conference: any;

  constructor(
    private route: ActivatedRoute,
    private conferencesService: ConferencesService
  ) { }

  ngOnInit() {
    this.getConference();
  }

  getConference(): void{
    const id = +this.route.snapshot.paramMap.get('id');
    this.conferencesService.get(id)
      .subscribe(conference => this.conference = conference);
  }


}
