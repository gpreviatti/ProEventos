import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LotesDetalhesComponent } from '@app/components/lotes/lotes-detalhes/lotes-detalhes.component';
import { FormHelper } from '@app/helpers/FormHelper';
import { RouterHelper } from '@app/helpers/RouterHelper';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-eventos-datalhe',
  templateUrl: './eventos-datalhe.component.html',
  styleUrls: ['./eventos-datalhe.component.scss']
})
export class EventosDatalheComponent implements OnInit
{
  public bsModalRef?: BsModalRef;
  public eventoId : any;
  public file : any;
  public imageUrl = 'assets/upload.png';

  constructor(
    private formBuilder: FormBuilder,
    public formHelper: FormHelper,
    private activatedrouter: ActivatedRoute,
    private eventoService : EventoService,
    private spinner : NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private routerHelper: RouterHelper
  ) { }

  ngOnInit(): void
  {
    this.carregarEvento();
  }

  public evento = {} as Evento;

  form = this.formBuilder.group({
    tema: ['', [Validators.required, Validators.minLength(4), Validators.max(50)]],
    local: ['', Validators.required, ],
    dataEvento: ['', Validators.required ],
    qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
    telefone: ['', Validators.required ],
    email: ['', [Validators.required, Validators.email]],
    imagemURL: ['']
  });

  public formControls = this.form.controls;

  public carregarEvento() : void {
    this.eventoId = this.activatedrouter.snapshot.paramMap.get('id')

    if (this.eventoId != null) {
      this.spinner.show();
      this.eventoService.getById(+this.eventoId)
        .subscribe(
          (evento: Evento) => {
            this.evento = {...evento}
            this.form.patchValue(this.evento);
            if(this.evento.imagemURL !== '') {
              this.imageUrl = environment.apiUrl + 'Resources/Images/' + this.evento.imagemURL
            }
          },
          (error : any) => {
            this.toastr.error(error?.message, 'Erro ao carregar evento')
          }
        ).add(() => this.spinner.hide())
    }
  }

  public salvarAlteracao() : void
  {
    this.spinner.show();
    if (this.form.invalid) {
      this.toastr.error('Formulário invalido')
      this.spinner.hide();
      return
    }

    this.evento = {...this.form.value}

    if (this.eventoId)
      this.evento.id = +this.eventoId;

    this.eventoService[this.eventoId ? 'put' : 'post'](this.evento).subscribe(
      (evento : Evento) => {
        let message = 'cadastrado'
        if (this.eventoId != undefined)
          message = 'alterado';

        this.toastr.success(`Evento ${evento.tema} ${message} com sucesso`, 'Sucesso');
        this.evento.id = evento.id;
        this.routerHelper.reloadComponent(`/eventos/detalhe/${evento.id}`);
      },
      (error : any) => this.toastr.error(error?.title, 'Erro ao cadastrar/alterar evento'),
    ).add(() => this.spinner.hide())
  }

  //#region Modal Evento
  public temaAtual = '';

  public modalExcluirEvento(event: any, template: TemplateRef<any>, evento: Evento): void {
    event.stopPropagation();
    this.evento = evento;
    this.temaAtual = evento.tema;
    this.bsModalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirm(): void {
    this.bsModalRef?.hide();
    this.spinner.show();
    this.eventoService.delete(this.evento.id).subscribe(
      (result: boolean) => {
        if(result) {
          this.showSuccess(`Evento de ${this.temaAtual} deletado com Sucesso!`);
          this.routerHelper.reloadComponent('/eventos');
        }
      },
      (error : any) => this.toastr.error(error.errors, 'Erro ao deletar evento')
    ).add(() => this.spinner.hide());
  }

  public decline(): void {
    this.bsModalRef?.hide();
  }

  private showSuccess(mensagem: string): void {
    this.toastr.success(mensagem);
  }
  //#endregion

  public abrirModalLotes() : void
  {
    const initialState = {
      eventoId: this.eventoId
    };
    this.bsModalRef = this.modalService.show(
      LotesDetalhesComponent,
      {initialState}
    );
  }

  // Evento para atualizar a imagem
  public onFileChange(event: any) : void
  {
    const reader = new FileReader();

    reader.onload = (e : any) => this.imageUrl = e.target.result;

    this.file = event.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImage()
  }

  public uploadImage() : void
  {
    this.spinner.show();

    this.eventoService.postUpload(this.eventoId, this.file).subscribe(
      () => {
        this.routerHelper.reloadComponent(`/eventos/detalhe/${this.eventoId}`);
        this.showSuccess(`Imagem inserida com sucesso!`)
      },
      (error : any) => this.toastr.error(error.errors, 'Erro ao inserir imagem')
    ).add(() => this.spinner.hide());
  }
}
