import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Request } from '../../services/request';

import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { forEach } from '@angular/router/src/utils/collection';
import swal from 'sweetalert2';
import { Constants } from '../../services/constants';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class
 HomeComponent implements OnInit {
  form;

  constructor(private request: Request
    , private fb: FormBuilder
    , private routes: Router
    , private data: DataService
    , private modalService: NgbModal) {
  }

  ngOnInit() {
    
  }

}
