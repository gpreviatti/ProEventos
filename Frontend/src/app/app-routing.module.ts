import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';

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
import { PalestranteListaComponent } from './components/palestrantes/palestrante-lista/palestrante-lista.component';
import { PalestranteDetalheComponent } from './components/palestrantes/palestrante-detalhe/palestrante-detalhe.component';

// Respeitar a precedencia
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'registration', component: RegistrationComponent },
  {
    // Cria um guardião para as rotas abaixo
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'user', component: UserComponent,
        children: [
          { path: 'perfil', component: PerfilComponent }
        ]
      },
      {
        path: 'eventos', component: EventosComponent,
        children: [
          { path: 'detalhe/:id', component: EventosDatalheComponent },
          { path: 'detalhe', component: EventosDatalheComponent },
          { path: 'lista', component: EventosListaComponent },
          { path: '', redirectTo: 'lista', pathMatch: 'full' }
        ],
      },
      {
        path: 'palestrantes', component: PalestrantesComponent,
        children: [
          { path: 'detalhe/:id', component: PalestranteDetalheComponent },
          { path: 'detalhe', component: PalestranteDetalheComponent },
          { path: 'lista', component: PalestranteListaComponent },
        ]
      },
      { path: 'contatos', component: ContatoComponent },
      { path: 'perfil', component: PerfilComponent },
    ]
  },
  { path: 'notfound', component: NotfoundComponent },
  { path: '**', redirectTo: 'notfound', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
