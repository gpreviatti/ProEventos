import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxCurrencyModule } from 'ngx-currency';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { EventoService } from './services/evento.service';
import { LoteService } from './services/lote.service';
import { PalestranteService } from './services/palestrante.service';
import { RedeSocialService } from './services/rede-social.service';

import { FormHelper } from './helpers/FormHelper';
import { RouterHelper } from './helpers/RouterHelper';

import { TituloComponent } from './shared/titulo/titulo.component';
import { NavComponent } from './shared/nav/nav.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventosDatalheComponent } from './components/eventos/eventos-datalhe/eventos-datalhe.component';
import { EventosListaComponent } from './components/eventos/eventos-lista/eventos-lista.component';
import { LotesComponent } from './components/lotes/lotes.component';
import { LotesDetalhesComponent } from './components/lotes/lotes-detalhes/lotes-detalhes.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { PalestranteDetalheComponent } from './components/palestrantes/palestrante-detalhe/palestrante-detalhe.component';
import { PalestranteListaComponent } from './components/palestrantes/palestrante-lista/palestrante-lista.component';
import { RedeSocialComponent } from './components/rede-social/rede-social.component';
import { RedeSocialDetalheComponent } from './components/rede-social/rede-social-detalhe/rede-social-detalhe.component';

import { PerfilComponent } from './components/user/perfil/perfil.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { ContatoComponent } from './components/contato/contato.component';

@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    EventosDatalheComponent,
    EventosListaComponent,
    PalestrantesComponent,
    NavComponent,
    TituloComponent,
    ContatoComponent,
    PerfilComponent,
    RegistrationComponent,
    UserComponent,
    LoginComponent,
    NotfoundComponent,
    LotesComponent,
    LotesDetalhesComponent,
    PalestranteDetalheComponent,
    PalestranteListaComponent,
    RedeSocialComponent,
    RedeSocialDetalheComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    NgxCurrencyModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    })
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    EventoService,
    LoteService,
    PalestranteService,
    RedeSocialService,
    FormHelper,
    RouterHelper
  ],
  bootstrap: [AppComponent],
  entryComponents: [LotesDetalhesComponent]
})
export class AppModule { }
