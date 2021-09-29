import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit
{
  modalRef?: BsModalRef;

  public eventosFiltrados: Evento[] = [];
  public eventos: Evento[] = [];
  public widthImg: number = 150;
  public marginImg: number = 2;
  public showImg: boolean = true;
  public temaAtual: string = '';

  private _filtroLista : string = '';

  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(v : string) {
    this._filtroLista = v;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  public ngOnInit(): void
  {
    this.getEventos();
  }

  public getEventos(): any
  {
    this.spinner.show();
    this.eventoService
      .getEventos()
      .subscribe({
        next: (eventos : Evento[]) => {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos
        },
        error: (error:any) => {
          this.spinner.hide()
          this.toastr.error(error.message, 'Erro!')
        },
        complete: () => this.spinner.hide()
      })
  }

  public alterarImage(): any
  {
    this.showImg = !this.showImg;
  }

  public filtrarEventos(filtrarPor : string) : Evento[]
  {
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      (evento : Evento) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
          evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  //#region Modal
  public openModal(template: TemplateRef<any>, evento: Evento) : void
  {
    this.temaAtual = evento.tema;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void
  {
    this.showSuccess(`Evento de ${this.temaAtual} deletado com Sucesso!`)
    this.modalRef?.hide();
  }

  public decline(): void
  {
    this.modalRef?.hide();
  }

  private showSuccess(mensagem : string) : void
  {
    this.toastr.success(mensagem);
  }
  //#endregion
}
