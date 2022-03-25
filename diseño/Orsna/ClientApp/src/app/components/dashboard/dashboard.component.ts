import { Component, OnInit } from '@angular/core';

import { Request } from '../../services/request';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor( private request: Request) { }
   resp: any;
  ngOnInit() {

  }
  ngAfterViewChecked() {
   /* window.scrollTo(0, 0);*/
  }
}
