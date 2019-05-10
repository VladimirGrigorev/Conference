import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-modal-file-upload',
  templateUrl: './modal-file-upload.component.html',
  styleUrls: ['./modal-file-upload.component.css']
})
export class ModalFileUploadComponent implements OnInit {

  @Input()
  lectureId: number;

  constructor() { }

  ngOnInit() {
  }

}
