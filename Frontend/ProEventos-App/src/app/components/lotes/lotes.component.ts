import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LotesDetalhesComponent } from './lotes-detalhes/lotes-detalhes.component';

@Component({
  selector: 'app-lotes',
  templateUrl: './lotes.component.html',
  styleUrls: ['./lotes.component.scss']
})
export class LotesComponent implements OnInit {
  public bsModalRef?: BsModalRef;
  public lotes = [] as Lote[];

  constructor(
    private loteService: LoteService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ) { }

  @Input() eventoId = 0;
  ngOnInit(): void {
    this.carregar();
  }

  public carregar(): void {
    this.spinner.show();
    this.loteService.getByEventoById(this.eventoId).subscribe(
      (lotes: Lote[]) => this.lotes = lotes,
      (error: any) => this.toastr.error(error.message, 'Erro!')
    ).add(() => this.spinner.hide());
  }

  public detalhar(lote: Lote): void {
    const initialState = {
      eventoId : this.eventoId,
      lote: lote
    };
    this.bsModalRef = this.modalService.show(
      LotesDetalhesComponent,
      { initialState }
    );
  }
}
