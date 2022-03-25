import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

import { AuthguardService } from './services/authguard.service';
import { HttpService } from './services/http-service.service';

//components
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavbarComponent } from './components/navbar/navbar.component';

import { AppComponent } from './app.component';
import { BeneficiariosComponent } from './components/beneficiarios/beneficiarios.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { CuentasComponent } from './components/cuentas/cuentas.component';
import { ProyectosComponent } from './components/proyectos/proyectos.component';
import { ConsultaCuentasComponent } from './components/consulta-cuentas/consulta-cuentas.component';
import { ConsultaBeneficariosComponent } from './components/consulta-beneficiarios/consulta-beneficiarios.component';
import { ConsultaUsuariosComponent } from './components/consulta-usuarios/consulta-usuarios.component';
import { ConsultaProyectosComponent } from './components/consulta-proyectos/consulta-proyectos.component';
import { AreasComponent } from './components/areas/areas.component';
import { ConsultaAreasComponent } from './components/consulta-areas/consulta-areas.component';
import { ConsultaLibranzasComponent } from './components/consulta-libranzas/consulta-libranzas.component';
import { LibranzasComponent } from './components/libranzas/libranzas.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    NavbarComponent,
    BeneficiariosComponent,
    UsuariosComponent,
    CuentasComponent,
    ProyectosComponent,
    ConsultaCuentasComponent,
    ConsultaBeneficariosComponent,
    ConsultaUsuariosComponent,
    ConsultaProyectosComponent,
    AreasComponent,
    ConsultaAreasComponent,
    ConsultaLibranzasComponent,
    LibranzasComponent  
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
    ReactiveFormsModule
  ],
  providers: [
    AuthguardService,
    HttpService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
