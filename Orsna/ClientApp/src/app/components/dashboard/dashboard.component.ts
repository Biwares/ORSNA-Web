import { Component, OnInit } from '@angular/core';

import { Request } from '../../services/request';
import { Constants } from '../../services/constants';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor( private request: Request) { }
  resp: any;
  puedeVer: boolean = false;

  ngOnInit() {
    this.request.get(Constants.API_SEGURIDAD + Constants.API_GET_PERMISOS_VER).subscribe(resp => {
      this.puedeVer = resp;
    });
  }
  ngAfterViewChecked() {
   /* window.scrollTo(0, 0);*/
  }
}
