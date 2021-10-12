import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserComponent } from './components/user/user.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { NotfoundComponent } from './components/notfound/notfound.component';

import { EventosDatalheComponent } from './components/eventos/eventos-datalhe/eventos-datalhe.component';
import { EventosListaComponent } from './components/eventos/eventos-lista/eventos-lista.component';
import { EventosComponent } from './components/eventos/eventos.component';

import { ContatoComponent } from './components/contato/contato.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';

// Respeitar a precedencia
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  {
      path: 'user', component: UserComponent,
      children: [
        { path: 'perfil', component: PerfilComponent }
      ]
  },
  { path: 'eventos', redirectTo: 'eventos/lista' },
  {
    path: 'eventos', component: EventosComponent,
    children: [
      { path: 'detalhe/:id', component: EventosDatalheComponent },
      { path: 'detalhe', component: EventosDatalheComponent },
      { path: 'lista', component: EventosListaComponent },
    ]
  },
  { path: 'palestrantes', component: PalestrantesComponent },
  { path: 'contatos', component: ContatoComponent },
  { path: 'perfil', component: PerfilComponent },
  { path: 'notfound', component: NotfoundComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'notfound', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
