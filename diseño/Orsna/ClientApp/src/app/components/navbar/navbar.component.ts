import { Component, OnInit, Inject } from '@angular/core';
import { Constants } from '../../services/constants';
import { DOCUMENT } from '@angular/common';
import { ConstantPool } from '@angular/compiler';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  url: string = Constants.URL_BASE;
  baseDir: string = Constants.base;
  
constructor(
    @Inject(DOCUMENT) private document: Document) { }

  ngOnInit() {
    this.document.body.classList.remove('OrsnaMainL');
  }

}
