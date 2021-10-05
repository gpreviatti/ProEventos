import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-eventos-datalhe',
  templateUrl: './eventos-datalhe.component.html',
  styleUrls: ['./eventos-datalhe.component.scss']
})
export class EventosDatalheComponent implements OnInit
{
  form = this.formBuilder.group({
    tema: ['', [Validators.required, Validators.minLength(4), Validators.max(50)]],
    local: ['', Validators.required, ],
    dataEvento: ['', Validators.required ],
    qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
    telefone: ['', Validators.required ],
    email: ['', [Validators.required, Validators.email]],
    imagemURL: ['', Validators.required ]
  });

  public formControls = this.form.controls;
  public evento = {} as Evento;


  constructor(
    private formBuilder: FormBuilder,
    public validators: ValidatorField,
    private activatedrouter: ActivatedRoute,
    private router: Router,
    private eventoService : EventoService,
    private spinner : NgxSpinnerService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void
  {
    this.carregarEvento();
  }

  public carregarEvento() : void {
    const eventoIdParam = this.activatedrouter.snapshot.paramMap.get('id')
    if (eventoIdParam != null) {
      this.spinner.show();
      this.eventoService.getById(+eventoIdParam)
        .subscribe(
          (evento: Evento) => {
            this.evento = {...evento}
            this.form.patchValue(this.evento);
          },
          (error : any) => {
            console.log(error)
            this.spinner.hide()
            this.toastr.error(error?.message, 'Erro ao carregar evento')
          },
          () => {this.spinner.hide()}
        )
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
    let id = this.activatedrouter.snapshot.paramMap.get('id')
    if (id) {
      this.eventoService.put(+id, this.evento).subscribe(
        (evento : Evento) => {
          this.form.patchValue(evento);
          this.toastr.success(`Evento ${evento.tema} atualizado com sucesso`, 'Sucesso')
          this.router.navigate([`/eventos/`]);
        },
        (error : any) => {
          this.spinner.hide()
          this.toastr.error(error?.title, 'Erro ao alterar evento')
        },
        () => this.spinner.hide(),
      )
      return
    }

    this.eventoService.post(this.evento).subscribe(
      (evento : Evento) => {
        this.toastr.success(`Evento ${evento.tema} salvo com sucesso`, 'Sucesso');
        this.router.navigate([`/eventos/`]);
      },
      (error : any) => {
        this.spinner.hide()
        this.toastr.error(error?.title, 'Erro ao cadastrar evento')
      },
      () => this.spinner.hide(),
    )
  }

  public resetForm() : void
  {
    this.form.reset();
  }
}
