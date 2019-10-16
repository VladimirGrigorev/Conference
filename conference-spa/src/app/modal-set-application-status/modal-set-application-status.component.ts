import { Component, OnInit, Input } from '@angular/core';
import { ApplicationStatusInfo } from '../application';

@Component({
  selector: 'app-modal-set-application-status',
  templateUrl: './modal-set-application-status.component.html',
  styleUrls: ['./modal-set-application-status.component.css']
})
export class ModalSetApplicationStatusComponent implements OnInit {

  @Input()
  app: ApplicationStatusInfo;
  
  constructor() { }

  ngOnInit() {
  }

}
