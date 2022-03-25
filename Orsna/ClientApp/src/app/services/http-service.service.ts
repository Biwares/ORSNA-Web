import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { Constants } from "../services/constants";
import { map } from 'rxjs/operators';

@Injectable()
export class HttpService {
  headers = new Headers();
  headerUser: string;
  constructor(private http: Http) {
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
            let jsonResponse = (res.json());
            return jsonResponse || {};
        }
        return {};
    }
}
