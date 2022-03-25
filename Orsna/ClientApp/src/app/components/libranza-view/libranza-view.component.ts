import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute  } from '@angular/router';
import { Request } from '../../services/request';
import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';

import { Constants } from '../../services/constants';

@Component({
  selector: 'app-libranza-view',
  templateUrl: './libranza-view.component.html',
  styleUrls: ['./libranza-view.component.css']
})
export class LibranzaViewComponent implements OnInit {


  constructor(private request: Request
    , public readonly swalTargets: SwalPartialTargets
    , private route: ActivatedRoute) { }
  lib: any = null;
  date: any = Date.now();
  id: string = this.route.snapshot.params['id'];
  baseDir: string = Constants.base;
  ngOnInit() {
    this.request.get("/libranza/GetLibranzaById?Id=" + this.id).subscribe(lib => {
      this.lib = lib;
    });
  }

}
