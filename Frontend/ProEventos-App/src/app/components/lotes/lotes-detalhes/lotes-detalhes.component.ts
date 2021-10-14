import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormHelper } from '@app/helpers/FormHelper';
import { RouterHelper } from '@app/helpers/RouterHelper';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef} from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-lotes-detalhes',
  templateUrl: './lotes-detalhes.component.html',
  styleUrls: ['./lotes-detalhes.component.scss']
})
export class LotesDetalhesComponent implements OnInit {

  public bsModalRefLotes?: BsModalRef;
  public eventoId = 0;
  public lote = {} as Lote;

  public formLotes = this.formBuilderLotes.group({
    nome : ['', [Validators.required, Validators.minLength(4), Validators.max(100)]],
    quantidade: ['', [Validators.required, Validators.max(99999)]],
    preco: ['', [Validators.required]],
    dataInicio: ['', [Validators.required]],
    dataFim: ['', [Validators.required]]
  });

  public formLoteControls = this.formLotes.controls;

  constructor(
    private loteService: LoteService,
    private formBuilderLotes: FormBuilder,
    public formHelper: FormHelper,
    public bsModalRef: BsModalRef,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private routerHelper: RouterHelper,
  ) { }

  ngOnInit(): void {
    if (this.lote) {
      this.detalhar();
    }
  }

  public detalhar(): void {
    this.formLotes.patchValue(this.lote);
  }

  public cadastrarAlterar(): void {
    this.spinner.show();
    if (this.formLotes.invalid || this.eventoId == null) {
      this.toastr.error('Formulário invalido');
      this.spinner.hide();
      return;
    }

    const lote = {...this.formLotes.value} as Lote;
    lote.eventoId = this.eventoId;

    if (this.lote.id !== undefined) {}
      lote.id = this.lote.id;

    this.loteService.put(lote.eventoId, lote).subscribe(
      (loteResponse: Lote) => {
        if (loteResponse) {
          this.toastr.success('Lote criado com sucesso', 'Sucesso!');
          this.bsModalRef.hide();
          this.routerHelper.reloadComponent(`/eventos/detalhe/${this.eventoId}`);
        }
      },
      (error: any) => this.toastr.error(error.message, 'Error!')
    ).add(() => this.spinner.hide());
  }

  public deletar(): void {
    this.spinner.show();
    this.loteService.delete(this.lote.eventoId, this.lote.id).subscribe(
      (response: boolean) => {
        if (response) {
          this.toastr.success('Lote removido com sucesso', 'Sucesso!');
          this.bsModalRef.hide();
          this.routerHelper.reloadComponent(`/eventos/detalhe/${this.eventoId}`);
        }
      },
      (error: any) => this.toastr.error(error.message, 'Error!')
    ).add(() => this.spinner.hide());
  }
}
