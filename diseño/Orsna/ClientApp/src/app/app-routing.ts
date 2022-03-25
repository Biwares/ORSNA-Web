import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//component
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
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
import { AuthguardService } from './services/authguard.service';
import { ConsultaLibranzasComponent } from './components/consulta-libranzas/consulta-libranzas.component';
import { LibranzasComponent } from './components/libranzas/libranzas.component';


const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard', component: DashboardComponent,
    children: [
      { path: '',  redirectTo: 'beneficiarios', pathMatch: 'full' },
      { path: 'beneficiario', canActivate: [AuthguardService], component: BeneficiariosComponent },
      { path: 'beneficiarios', canActivate: [AuthguardService], component: ConsultaBeneficariosComponent },
      { path: 'cuenta', canActivate: [AuthguardService], component: CuentasComponent },     
      { path: 'cuentas', canActivate: [AuthguardService], component: ConsultaCuentasComponent },
      { path: 'usuario', canActivate: [AuthguardService], component: UsuariosComponent },
      { path: 'usuarios', canActivate: [AuthguardService], component: ConsultaUsuariosComponent },
      { path: 'proyecto', canActivate: [AuthguardService], component: ProyectosComponent },
      { path: 'proyectos', canActivate: [AuthguardService], component: ConsultaProyectosComponent },
      { path: 'area', canActivate: [AuthguardService], component: AreasComponent },
      { path: 'areas', canActivate: [AuthguardService], component: ConsultaAreasComponent },
      { path: 'libranza', canActivate: [AuthguardService], component: LibranzasComponent },
      { path: 'libranzas', canActivate: [AuthguardService], component: ConsultaLibranzasComponent }
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


