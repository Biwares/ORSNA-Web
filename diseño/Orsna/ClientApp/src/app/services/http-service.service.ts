import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { Constants } from "../services/constants";
//import 'rxjs/Rx';
import { map } from 'rxjs/operators';
//import 'rxjs/add/operator/toPromise';

@Injectable()
export class HttpService {
  headers = new Headers();
  headerUser: string;
  constructor(private http: Http) {
    this.headerUser = sessionStorage.getItem("usuario_userid") + ";" + sessionStorage.getItem("user");
    this.headers.append('Content-Type', 'application/json');
    this.headers.append('authentication', this.headerUser);
  } 
    
  get(url: string): Observable<any> {
    return this.http.get(Constants.URL_BASE_WEB_API + url, { headers: this.headers }).pipe(map(resp => { return this.extractData(resp, null); }));
    }   

    delete(url: string): Observable<any> {
      return this.http.delete(url, { headers: this.headers }).pipe(map(resp => { return this.extractData(resp, null); }));
    }

    post(url: string, body: any): Observable<any> {

        return this.http.post(Constants.URL_BASE_WEB_API + url, body, { headers: this.headers }).pipe(resp => { return resp; });
    }

    postFileUpload(url: string, data: any): Observable<any> {
      return this.http.post(Constants.URL_BASE_WEB_API + url, data, { headers: this.headers }).pipe(map(resp => { return this.extractData(resp, null); }));
    }

    private handleError(error: any, router: Router) {
        if (error.status == 401) {
            router.navigate(['/login']);
        }
        return Observable.throw(error);
    }

    private extractData(res: any, router: Router) {
        if (res._body != "") {
            if (res.status == 401) {
                if (router == undefined || router == null)
                    throw new Error('Bad response status: ' + res.status);
                else
                    router.navigate(['/login']);
            }
            if (res.status < 200 || res.status >= 300) {
                throw new Error('Bad response status: ' + res.status);
            }
            // console.log(res.json());
            let jsonResponse = (res.json());
            return jsonResponse || {};
        }
        return {};
    }
}
