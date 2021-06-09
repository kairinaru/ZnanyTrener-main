import { Component, Input, OnInit } from '@angular/core';
import { UserDetail } from '../_models/userDetail';

@Component({
  selector: 'app-coach-preview',
  templateUrl: './coach-preview.component.html',
  styleUrls: ['./coach-preview.component.css']
})
export class CoachPreviewComponent implements OnInit {

  @Input() coach: UserDetail;
  
  constructor() { }

  ngOnInit(): void {
  }

}
