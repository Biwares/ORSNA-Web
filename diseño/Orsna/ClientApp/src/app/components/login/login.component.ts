//ng generate component login
import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Request } from '../../services/request';
import { Router } from '@angular/router';
import { Constants } from '../../services/constants';
import swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  url: string = Constants.URL_BASE;
  user: string;
  password: string;
  baseDir: string = Constants.base;
  loading: boolean = false;
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private request: Request,
    private routes: Router) { }

  ngOnInit() {
    sessionStorage.removeItem('user');
    sessionStorage.removeItem('usuario_userid');
    this.document.body.classList.add('OrsnaMainL');
  }

  async LoginClick() {
    if (this.user != "" && this.password != "") {
      this.loading = true;
      let promise = await new Promise(() => {
        this.request.post("/Login/Do", { user: this.user, password: this.password })
          .subscribe(resp => {
            if (resp.result.estado == true) {
              sessionStorage.setItem('user', resp.result.username);
              sessionStorage.setItem('usuario_userid', resp.result.idUsuario);


              this.routes.navigate(['../dashboard']);
              //window.location.href = ('dashboard');
            } else {
              swal({
                type: 'error',
                title: 'Intente de Nuevo',
                text: 'Usuario o clave incorrecta!'
              })
            }
            this.loading = false;
          });
      });
    }
  }
}
