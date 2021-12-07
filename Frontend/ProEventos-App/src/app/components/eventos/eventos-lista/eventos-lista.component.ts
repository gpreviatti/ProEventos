import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { environment } from '@environments/environment';
import { PaginatedRequest } from '@app/messages/PaginatedRequest';
import { PaginatedResponse } from '@app/messages/PaginatedResponse';

@Component({
  selector: 'app-eventos-lista',
  templateUrl: './eventos-lista.component.html',
  styleUrls: ['./eventos-lista.component.scss']
})
export class EventosListaComponent implements OnInit {
  modalRef?: BsModalRef;

  public eventosFiltrados: Evento[] = [];
  public evento = {} as Evento;
  public eventos: Evento[] = [];
  public widthImg = 100;
  public marginImg = 2;
  public showImg = true;
  public temaAtual = '';
  public paginatedRequest = {} as PaginatedRequest;

  private _filtroLista = '';

  // public get filtroLista(): string {
  //   return this._filtroLista;
  // }
  // public set filtroLista(v: string) {
  //   this._filtroLista = v;
  //   this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  // }

  constructor(
    private eventoService: EventoService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.paginatedRequest = {
      currentPage: 1,
      pageSize: 10,
      totalItems: 100,
      totalPages: 1
    };

    this.getEventos();
  }

  public getEventos(): any {
    this.spinner.show();
    this.eventoService
      .getPaginated(this.paginatedRequest)
      .subscribe(
        (response: PaginatedResponse<Evento[]>) => {
          this.eventos = response.data;
          this.eventosFiltrados = this.eventos;

          this.paginatedRequest = {
            currentPage: response.currentPage,
            pageSize: response.pageSize,
            totalItems: response.recordsTotal,
            totalPages: response.recordsTotal
          } as PaginatedRequest;

        },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error(error.message, 'Erro!');
        },
        () => this.spinner.hide()
      );
  }

  public pageChanged(event: any): void {
    this.paginatedRequest.currentPage = event.page;
    this.getEventos();
  }

  public alterarImage(): any {
    this.showImg = !this.showImg;
  }

  public filtrarEventos(filtrarPor: any): void {
    this.paginatedRequest.searchValue = filtrarPor;
    this.getEventos();
  }

  public detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }

  public showImage(imagemURL: any) {
    if (imagemURL === '') {
      return '/assets/upload.png';
    }
    return environment.apiUrl + 'Resources/Images/Eventos/' + imagemURL;
  }
}
