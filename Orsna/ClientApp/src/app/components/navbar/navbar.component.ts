import { Component, OnInit, Inject } from '@angular/core';
import { Constants } from '../../services/constants';
import { DOCUMENT } from '@angular/common';
import { Request } from '../../services/request';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpService } from "../../services/http-service.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  url: string = Constants.URL_BASE;
  baseDir: string = Constants.base;
  loggedUser: string = '';

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private request: Request,
    private routes: Router,
    private http: HttpService) { }



  ngOnInit() {
    this.document.body.classList.remove('OrsnaMainL');
    this.loggedUser = JSON.parse(sessionStorage.getItem('currentUser')).email;
  }
  logout() {
    this.request.get('/login/Logout').subscribe(resp => {
      if (resp == "OK") {
        sessionStorage.clear();
        this.routes.navigate(['../login']);
      }
    });
  }
}

