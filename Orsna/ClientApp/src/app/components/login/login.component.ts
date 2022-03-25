//ng generate component login
import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Request } from '../../services/request';
import { Router, ActivatedRoute } from '@angular/router';
import { Constants } from '../../services/constants';
import swal from 'sweetalert2';
import { HttpService } from "../../services/http-service.service";
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthenticationService } from '../../services';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  url: string = Constants.URL_BASE;
  user: string;
  password: string;
  baseDir: string = Constants.base;
  loading: boolean = false;
  submitted = false;
  returnUrl: string;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    @Inject(DOCUMENT) private document: Document,
    private request: Request,
    private router: Router,
    private route: ActivatedRoute,
    private http: HttpService,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.document.body.classList.add('OrsnaMainL');

  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    //stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.login(this.f.Email.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.loading = false;
          this.router.navigate(['../dashboard']);
          //this.router.navigate([this.returnUrl]);
        },
        error => {
          swal({
            type: 'error',
            title: 'Intente de Nuevo',
            text: 'Usuario o clave incorrecta!'
          })
          //this.error = error;
          this.loading = false;
        });
  }
}
