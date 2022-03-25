import { Injectable } from '@angular/core';
import { Request } from './request';
import { Audit } from '../Models/index';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class AuditService {

  private request: Request;

  constructor(private req: Request) {
    this.request = req;
  }

  public getAll(page: number, filterFechaDesde: string, filterFechaHasta: string, filterMensaje: string, filterDetalle: string, filterUserName: string, order: string, columnOrder: string): Observable<Audit[]> {
    return this.request.get("/Audit/GetAll?page=" + page.toString() + "&FilterFechaDesde=" + filterFechaDesde
      + "&FilterFechaHasta=" + filterFechaHasta + "&FilterMensaje=" + filterMensaje + "&FilterDetalle=" + filterDetalle + "&FilterUserName=" + filterUserName
      + "&Order=" + order + "&ColumnOrder=" + columnOrder)
      .pipe(
        map((data: any) => <Audit[]>data)
      );
  }

  public getCountPages(page: number, filterFechaDesde: string, filterFechaHasta: string, filterMensaje: string, filterDetalle: string, filterUserName: string, order: string, columnOrder: string): Observable<number> {
    return this.request.get("/Audit/GetCountPages?page=" + page.toString() + "&FilterFechaDesde=" + filterFechaDesde
      + "&FilterFechaHasta=" + filterFechaHasta + "&FilterMensaje=" + filterMensaje + "&FilterDetalle=" + filterDetalle + "&FilterUserName=" + filterUserName
      + "&Order=" + order + "&ColumnOrder=" + columnOrder)
      .pipe(
        map((data: any) => <number>data)
      );
  }
  public getCountFilterElements(page: number, filterFechaDesde: string, filterFechaHasta: string, filterMensaje: string, filterDetalle: string, filterUserName: string, order: string, columnOrder: string): Observable<number> {
    return this.request.get("/Audit/GetCountFilterElements?page=" + page.toString() + "&FilterFechaDesde=" + filterFechaDesde
      + "&FilterFechaHasta=" + filterFechaHasta + "&FilterMensaje=" + filterMensaje + "&FilterDetalle=" + filterDetalle + "&FilterUserName=" + filterUserName
      + "&Order=" + order + "&ColumnOrder=" + columnOrder)
      .pipe(
        map((data: any) => <number>data)
      );
  }
  public getAllWithoutFilters() {
    return this.request.get("/Audit/GetCountFilterElements?page=0").pipe(
      map((data: any) => <number>data)
    );
  }
}
