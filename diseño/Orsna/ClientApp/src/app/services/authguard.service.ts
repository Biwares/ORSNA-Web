import { NgModule, Component, Injectable } from '@angular/core';
import { Request } from './request';
import { Routes, RouterModule, CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { HttpService } from "../services/http-service.service";
import { Observable, of as observableOf } from 'rxjs';
import { map } from 'rxjs/operators';
//import 'rxjs/add/operator/toPromise'; DEPRECATE

@Injectable({
  providedIn: 'root'
})
export class AuthguardService implements CanActivate {
  result: string;
  constructor(
    private request: Request,
    private routes: Router,
    private http: HttpService
  ) { }

  canActivate(): Observable<boolean> | boolean {
    //return observableOf(true);
    if (sessionStorage.getItem("usuario_userid") != null && sessionStorage.getItem("user")) {
      return this.http.post("/login/Navigate",null)
        .pipe(map(auth => {
          //console.log(auth.ok);
          if (auth.ok == true) {
            return true;
          }
          return false;
        }));
    }
    this.routes.navigate(['/login']);
    return false;
  }/*
  search(): Promise<any> {
    const promise = new Promise((resolve, reject) => {
      const resp = this.http.post("/login/Navigate").toPromise();
      resp.then( async res => {
        this.result = await res;
        resolve();
      }, msg => {
        reject(msg)
      })
    });
      return promise;
    }*/
}
