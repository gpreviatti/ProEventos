import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { LotesComponent } from './components/lotes/lotes.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { NavComponent } from './shared/nav/nav.component';
import { CommonModule } from '@angular/common';
import { NgxSpinnerModule } from 'ngx-spinner';
import { TituloComponent } from './shared/titulo/titulo.component';
import { ContatoComponent } from './components/contato/contato.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { EventosDatalheComponent } from './components/eventos/eventos-datalhe/eventos-datalhe.component';
import { EventosListaComponent } from './components/eventos/eventos-lista/eventos-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NotfoundComponent } from './components/notfound/notfound.component';

import { EventoService } from './services/evento.service';
import { LoteService } from './services/lote.service';
import { FormHelper } from './helpers/FormHelper';

import { LotesDetalhesComponent } from './components/lotes/lotes-detalhes/lotes-detalhes.component';
import { RouterHelper } from './helpers/RouterHelper';
import { NgxCurrencyModule } from 'ngx-currency';

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
    LotesDetalhesComponent
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
    FormHelper,
    RouterHelper
  ],
  bootstrap: [AppComponent],
  entryComponents: [LotesDetalhesComponent]
})
export class AppModule { }
