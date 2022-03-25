import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID,NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

import { HttpService } from './services/http-service.service';
import { AuditService } from './services/index';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//components
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { BasicAuthInterceptor, ErrorInterceptor, CacheInterceptor } from './_helpers';

import { AppComponent } from './app.component';
import { BeneficiariosComponent } from './components/beneficiarios/beneficiarios.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { CuentasComponent } from './components/cuentas/cuentas.component';
import { ProyectosComponent } from './components/proyectos/proyectos.component';
import { ConsultaCuentasComponent } from './components/consulta-cuentas/consulta-cuentas.component';
import { ConsultaBeneficariosComponent } from './components/consulta-beneficiarios/consulta-beneficiarios.component';
import { ConsultaUsuariosComponent } from './components/consulta-usuarios/consulta-usuarios.component';
import { ConsultaProyectosComponent } from './components/consulta-proyectos/consulta-proyectos.component';
import { HomeComponent } from './components/home/home.component';
import { AreasComponent } from './components/areas/areas.component';
import { ConsultaAreasComponent } from './components/consulta-areas/consulta-areas.component';
import { ConsultaLibranzasComponent } from './components/consulta-libranzas/consulta-libranzas.component';
import { LibranzasComponent } from './components/libranzas/libranzas.component';
import { LibranzaViewComponent } from './components/libranza-view/libranza-view.component';
import { AuditComponent } from './components/index';
import { RolComponent } from './components/rol/rol.component';
import { ConsultaRolComponent } from './components/consulta-rol/consulta-rol.component';
import { RolModuloComponent } from './components/rolmodulo/rolmodulo.component';
import { ConsultaRolModuloComponent } from './components/consulta-rolmodulo/consulta-rolmodulo.component';
import { registerLocaleData } from '@angular/common';
import localeEsAr from '@angular/common/locales/es-AR';

import { TextMaskModule } from 'angular2-text-mask';
import { NgxJsonViewerModule } from 'ngx-json-viewer';

registerLocaleData(localeEsAr, 'es-Ar');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    NavbarComponent,
    BeneficiariosComponent,
    CuentasComponent,
    ProyectosComponent,
    ConsultaCuentasComponent,
    ConsultaBeneficariosComponent,
    ConsultaProyectosComponent,
    AreasComponent,
    ConsultaAreasComponent,
    ConsultaLibranzasComponent,
    LibranzasComponent,
    LibranzaViewComponent,
    AuditComponent,
    UsuariosComponent,
    ConsultaUsuariosComponent,
    ConsultaRolComponent,
    RolComponent,
    ConsultaRolModuloComponent,
    RolModuloComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    NgbModule,
    NgMultiSelectDropDownModule,
    SweetAlert2Module.forRoot(),
    ReactiveFormsModule,
    TextMaskModule,
    NgxJsonViewerModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: CacheInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: BasicAuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    HttpService,
    AuditService,
    { provide: LOCALE_ID, useValue: "es-AR" },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
