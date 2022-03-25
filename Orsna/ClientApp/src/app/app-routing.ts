import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//component
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { BeneficiariosComponent } from './components/beneficiarios/beneficiarios.component';
import { CuentasComponent } from './components/cuentas/cuentas.component';
import { ProyectosComponent } from './components/proyectos/proyectos.component';
import { ConsultaCuentasComponent } from './components/consulta-cuentas/consulta-cuentas.component';
import { ConsultaBeneficariosComponent } from './components/consulta-beneficiarios/consulta-beneficiarios.component';
import { ConsultaProyectosComponent } from './components/consulta-proyectos/consulta-proyectos.component';
import { AreasComponent } from './components/areas/areas.component';
import { ConsultaAreasComponent } from './components/consulta-areas/consulta-areas.component';
import { ConsultaLibranzasComponent } from './components/consulta-libranzas/consulta-libranzas.component';
import { LibranzasComponent } from './components/libranzas/libranzas.component';
import { LibranzaViewComponent } from './components/libranza-view/libranza-view.component';
import { AuditComponent } from './components/index';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { ConsultaUsuariosComponent } from './components/consulta-usuarios/consulta-usuarios.component';
import { RolComponent } from './components/rol/rol.component';
import { ConsultaRolComponent } from './components/consulta-rol/consulta-rol.component';
import { RolModuloComponent } from './components/rolmodulo/rolmodulo.component';
import { ConsultaRolModuloComponent } from './components/consulta-rolmodulo/consulta-rolmodulo.component';
import { AuthGuard } from './_guards';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard/home', pathMatch: 'full' },
  //{ path: '', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'libranzaview/:id', component: LibranzaViewComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', canActivate: [AuthGuard], component: HomeComponent },
      { path: 'beneficiario', canActivate: [AuthGuard], component: BeneficiariosComponent },
      { path: 'cuenta', canActivate: [AuthGuard], component: CuentasComponent },
      { path: 'proyecto', canActivate: [AuthGuard], component: ProyectosComponent },
      { path: 'cuentas', canActivate: [AuthGuard], component: ConsultaCuentasComponent },
      { path: 'beneficiarios', canActivate: [AuthGuard], component: ConsultaBeneficariosComponent },
      { path: 'proyectos', canActivate: [AuthGuard], component: ConsultaProyectosComponent },
      { path: 'area', canActivate: [AuthGuard], component: AreasComponent },
      { path: 'areas', canActivate: [AuthGuard], component: ConsultaAreasComponent },
      { path: 'libranza', canActivate: [AuthGuard], component: LibranzasComponent },
      { path: 'libranzas', canActivate: [AuthGuard], component: ConsultaLibranzasComponent },
      { path: 'audit', canActivate: [AuthGuard], component: AuditComponent },
      { path: 'usuarios', canActivate: [AuthGuard], component: ConsultaUsuariosComponent },
      { path: 'usuario', canActivate: [AuthGuard], component: UsuariosComponent },
      { path: 'roles', canActivate: [AuthGuard], component: ConsultaRolComponent },
      { path: 'rol', canActivate: [AuthGuard], component: RolComponent },
      { path: 'rolesmodulo', canActivate: [AuthGuard], component: ConsultaRolComponent },
      { path: 'rolmodulo', canActivate: [AuthGuard], component: RolComponent }
    ]
  }
];


@NgModule({
  exports: [RouterModule],
  imports: [
    RouterModule.forRoot(routes, { useHash: true })
  ]
})
export class AppRoutingModule { }
