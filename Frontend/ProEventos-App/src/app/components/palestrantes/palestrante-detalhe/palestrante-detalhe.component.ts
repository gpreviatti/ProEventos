import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { FormHelper } from '@app/helpers/FormHelper';
import { RouterHelper } from '@app/helpers/RouterHelper';
import { Palestrante } from '@app/models/Palestrante';
import { PalestranteService } from '@app/services/palestrante.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-palestrante-detalhe',
  templateUrl: './palestrante-detalhe.component.html',
  styleUrls: ['./palestrante-detalhe.component.scss']
})
export class PalestranteDetalheComponent implements OnInit {

  public bsModalRef?: BsModalRef;
  public palestranteId: any;
  public palestrante: any;

  form = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(4), Validators.max(50)]],
    miniCurriculo: [''],
    telefone: ['', Validators.required ],
    email: ['', [Validators.required, Validators.email]],
    imagemURL: ['']
  });

  public formControls = this.form.controls;

  constructor(
    private formBuilder: FormBuilder,
    public formHelper: FormHelper,
    private activatedrouter: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private routerHelper: RouterHelper,
    private palestranteService: PalestranteService
  ) { }

  ngOnInit(): void {
    this.carregarPalestrante();
  }

  public carregarPalestrante(): void {
    this.palestranteId = this.activatedrouter.snapshot.paramMap.get('id');

    if (this.palestranteId != null) {
      this.spinner.show();
      this.palestranteService.getById(+this.palestranteId)
        .subscribe(
          (palestrante: Palestrante) => {
            this.palestrante = {...palestrante};
            this.form.patchValue(this.palestrante);
          },
          (error: any) => {
            this.toastr.error(error?.message, 'Erro ao carregar evento');
          }
        ).add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.invalid) {
      this.toastr.error('Formulário invalido');
      this.spinner.hide();
      return;
    }

    this.palestrante = {...this.form.value};

    if (this.palestranteId) {
      this.palestrante.id = +this.palestranteId;
    }

    this.palestranteService.post(this.palestrante).subscribe(
      (palestrante: Palestrante) => {
        let message = 'cadastrado';
        if (this.palestranteId !== undefined) {
          message = 'alterado';
        }

        this.toastr.success(`Evento ${palestrante.nome} ${message} com sucesso`, 'Sucesso');
        this.palestranteId.id = palestrante.id;
        this.routerHelper.reloadComponent(`/eventos/detalhe/${palestrante.id}`);
      },
      (error: any) => this.toastr.error(error?.title, 'Erro ao cadastrar/alterar evento'),
    ).add(() => this.spinner.hide());
  }

  public modalExcluirPalestrante(event: any, template: TemplateRef<any>, palestrante: Palestrante): void {
    event.stopPropagation();
    this.palestrante = palestrante;
    this.bsModalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirm(): void {
    this.bsModalRef?.hide();
    this.spinner.show();
    this.palestranteService.delete(this.palestrante.id).subscribe(
      (result: boolean) => {
        if (result) {
          this.showSuccess(`Evento de ${this.palestrante.nome} deletado com Sucesso!`);
          this.routerHelper.reloadComponent('/eventos');
        }
      },
      (error: any) => this.toastr.error(error.errors, 'Erro ao deletar evento')
    ).add(() => this.spinner.hide());
  }

  public decline(): void {
    this.bsModalRef?.hide();
  }

  private showSuccess(mensagem: string): void {
    this.toastr.success(mensagem);
  }

}
