import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuditService } from '../../services/index';
import { Audit } from '../../Models/index';

@Component({
  selector: 'app-audit',
  templateUrl: './audit.component.html',
  styleUrls: ['./audit.component.css']
})

export class AuditComponent implements OnInit {

  page: number = 1;
  FilterUserName: string = "";
  FilterMensaje: string = "";
  FilterDetalle: string = "";
  FilterFechaDesde: string = "";
  FilterFechaHasta: string = "";
  Order: string = "desc";
  ColumnOrder: string = "Fecha";
  CountPages: number = 0;
  CountElements: number = 0;
  CountFilterElements: number = 0;

  public audits: Audit[];

  loading: boolean = false;
    
  constructor(private auditService: AuditService, private routes: Router) { }

  ngOnInit() {
    this.FilterUserName = "";
    this.FilterMensaje = "";
    this.FilterDetalle= "";
    this.FilterFechaDesde = "";
    this.FilterFechaHasta = "";
    this.Order = "desc";
    this.ColumnOrder = "Fecha";
    this.CountPages = 0;
    this.CountElements = 0;
    this.CountFilterElements= 0;

    this.GetAll(this.Order, this.ColumnOrder, 1);
  }

  refresh(): void {
    this.ngOnInit();
  }

  GetAllWithoutFilters() {
    this.auditService.getAllWithoutFilters()
      .subscribe(resp => {
        this.CountElements = resp;
      });
  }

  GetCountFilterElements(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.auditService.getCountFilterElements(this.page, this.FilterFechaDesde, this.FilterFechaHasta, this.FilterMensaje, this.FilterDetalle, this.FilterUserName, this.Order, this.ColumnOrder)
      .subscribe(resp => {
        this.CountFilterElements = resp;
      });
  }
  
  GetCountPages(order: string, columnOrder: string) {
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }
    this.auditService.getCountPages(this.page, this.FilterFechaDesde, this.FilterFechaHasta, this.FilterMensaje, this.FilterDetalle, this.FilterUserName, this.Order, this.ColumnOrder)
      .subscribe(resp => {
        this.CountPages = resp * 10;
      });
  }

  GetAll(order: string, columnOrder: string, event: number) {
    this.loading = true;
    this.page = event;
    this.GetAllWithoutFilters();
    this.GetCountFilterElements(this.Order, this.ColumnOrder);
    if (columnOrder != undefined && columnOrder != null && columnOrder != '' && columnOrder != "") {
      this.ColumnOrder = columnOrder;
    }
    if (order != undefined && order != null && order != '' && order != "") {
      this.Order = order;
    }

    this.auditService.getAll(this.page, this.FilterFechaDesde, this.FilterFechaHasta, this.FilterMensaje, this.FilterDetalle, this.FilterUserName, this.Order, this.ColumnOrder)
      .subscribe(resp => {
        this.audits = [];
        this.audits = resp;
        this.loading = false;
        
        this.audits.forEach(value => {
          value.detalleJson = this.ToJson(value.detalle);
        }); 
      });
  }
  
  ToJson (v:string):any {
    try{
      let json = {};
      let data = '';
      const anterior = 'Valor anterior: \r\n{';
      const nuevo = 'Valor Nuevo: \r\n{';
      json['accion'] = v.substring(0, v.indexOf('Valor anterior: '));

      if (v.indexOf(anterior) > 0) {
        data = v.substring(v.indexOf(anterior) + anterior.length -1, v.indexOf(nuevo));
        json['Valor anterior'] = JSON.parse(data);;
      }
      
      if (v.indexOf(nuevo) > 0) {
        data = v.substring(v.indexOf(nuevo) + nuevo.length -1);
        json['Valor Nuevo'] = JSON.parse(data);;
      }
      
      return json;
    }
    catch(e) {
      console.log('error occored while you were typing the JSON');
    };
  }

}
