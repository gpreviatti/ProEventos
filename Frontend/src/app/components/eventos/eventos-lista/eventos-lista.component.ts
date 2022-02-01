import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { environment } from '@environments/environment';
import { PaginatedRequest } from '@app/messages/PaginatedRequest';
import { PaginatedResponse } from '@app/messages/PaginatedResponse';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-eventos-lista',
  templateUrl: './eventos-lista.component.html',
  styleUrls: ['./eventos-lista.component.scss']
})
export class EventosListaComponent implements OnInit {
  modalRef?: BsModalRef;

  public evento = {} as Evento;
  public eventos: Evento[] = [];
  public widthImg = 100;
  public marginImg = 2;
  public showImg = true;
  public temaAtual = '';
  public paginatedRequest = {} as PaginatedRequest;
  public searchValueChanged: Subject<string> = new Subject<string>();

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
    if (this.searchValueChanged.observers.length === 0) {
      this.searchValueChanged.pipe(debounceTime(500))
      .subscribe(
        (filter) => {
          this.paginatedRequest.searchValue = filter;
          this.getEventos();
        }
      );
    }
    this.searchValueChanged.next(event.value);
  }

  public detalharEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }

  public alterarImage(): any {
    this.showImg = !this.showImg;
  }

  public showImage(imagemURL: any) {
    if (imagemURL === '') {
      return '/assets/upload.png';
    }
    return environment.apiUrl + 'Resources/Images/Eventos/' + imagemURL;
  }
}
