import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';
import { ToastrService } from 'ngx-toastr';

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

  private _temaAtual : string = '';
  public get temaAtual() : string {
    return this._temaAtual;
  }
  public set temaAtual(v : string) {
    this._temaAtual = v;
  }

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
    private toastr: ToastrService
  ) { }

  ngOnInit(): void
  {
    this.getEventos();
  }

  public getEventos(): any
  {
    this.eventoService
      .getEventos()
      .subscribe(
        (eventos : Evento[]) => {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos
        },
        error => console.log(error)
      )
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
  public openModal(template: TemplateRef<any>) : void
  {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void
  {
    this.showSuccess(`Evento ${this.temaAtual} deletado com Sucesso!`)
    this.modalRef?.hide();
  }

  public decline(): void
  {
    this.modalRef?.hide();
  }

  private showSuccess(mensagem : string) {
    this.toastr.success(mensagem);
  }
  //#endregion
}
