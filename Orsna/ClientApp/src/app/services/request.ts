import { Component, OnInit, Injectable } from '@angular/core';
import { Observable, Subject, from, throwError, of } from "rxjs";
import { map, filter, catchError, mergeMap } from 'rxjs/operators';

import { DataFile } from '../Models/DataFile';

import { HttpParams, HttpClient, HttpHeaders, HttpHandler } from "@angular/common/http";
import { Location, LocationStrategy } from '@angular/common';

import { Http, Response, HttpModule } from '@angular/http';

import { Constants } from "../services/constants";


const params = new HttpParams().set('_page', "1").set('_limit', "1");

@Injectable({ providedIn: 'root' })
export class Request {

  location: Location;
  private subject = new Subject<any>();
  private headers: HttpHeaders;
  constructor(private Http: HttpClient, location: Location) {
    this.location = location;
  }

  downloadFile(url: string, filename: string = null): void {
    this.getWithResponseType(url, 'arraybuffer').subscribe(
      (response: any) => {
        let dataType = response.type;
        let binaryData = [];
        binaryData.push(response);
        let downloadLink = document.createElement('a');
        downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, { type: dataType }));
        downloadLink.setAttribute("target", "_blank");
        if (filename)
          downloadLink.setAttribute('download', filename);
        document.body.appendChild(downloadLink);
        downloadLink.click();
        downloadLink.parentNode.removeChild(downloadLink);
      }
    )
  }

  getWithResponseType(url: string,rt: any): Observable<any> {
    return this.Http.get(Constants.URL_BASE_WEB_API + url, { responseType: rt, headers: this.headers })
      .pipe(
        map((data: any[]) => {
          return data
        })
        , catchError(error => {
          return throwError('Algo salio mal!')
        })
      );
  }

  get(url: string): Observable<any> {
    return this.Http.get(Constants.URL_BASE_WEB_API + url, { headers: this.headers })
      .pipe(
        map((data: any[]) => {
          return data
        })
        , catchError(error => {
          return throwError('Algo salio mal!')
        })
      );
  }
  post(url: string, data: any): Observable<any> {
    return this.Http.post(Constants.URL_BASE_WEB_API + url, data, { headers: this.headers })
      .pipe(
        map((data: any[]) => {
          return data
        })
        , catchError(error => {
          return throwError('Algo salio mal!')
        })
      );
  }

  put(url: string, data: any): Observable<any> {
    return this.Http.put(Constants.URL_BASE_WEB_API + url, data, { headers: this.headers })
      .pipe(
        map((data: any[]) => {
          return data
        })
        , catchError(error => {
          return throwError('Algo salio mal!')
        })
      );
  }
  delete(url: string): Observable<any> {
    return this.Http.delete(Constants.URL_BASE_WEB_API + url, { headers: this.headers })
      .pipe(
        map((data: any[]) => {
          return data
        })
        , catchError(error => {
          return throwError('Algo salio mal!')
        })
      );
  }
  createDataFile(url: string, dataFile: FormData): Observable<any> {
    return this.Http
      .post<DataFile>(Constants.URL_BASE_WEB_API + url, dataFile)
      .pipe(catchError(this.handleError<DataFile>(`createFile`)));
  }
  updateDataFile(url: string, dataFile: FormData): Observable<any> {
    return this.Http
      .put<DataFile>(url, dataFile)
      .pipe(catchError(this.handleError<DataFile>(`updateFile`)));
  }
  deleteDataFile(url: string): Observable<any> {
    return this.Http
      .delete<DataFile>(url)
      .pipe(catchError(this.handleError<DataFile>(`updateFile`)));
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
