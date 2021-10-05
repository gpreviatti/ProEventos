import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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
    imagemUrl: ['', Validators.required ]
  });

  public formControls = this.form.controls;
  public evento = {} as Evento;


  constructor(
    private formBuilder: FormBuilder,
    public validators: ValidatorField,
    private router: ActivatedRoute,
    private eventoService : EventoService,
    private spinner : NgxSpinnerService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void
  {
    this.carregarEvento();
  }

  public carregarEvento() : void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id')
    if (eventoIdParam != null) {
      this.spinner.show();
      this.eventoService.getById(+eventoIdParam)
        .subscribe(
          (evento: Evento) => {
            this.evento = {...evento}
            this.form.patchValue(this.evento);
          },
          (error : any) => {
            this.spinner.hide()
            this.toastr.error(error.message, 'Erro ao carregar evento')
          },
          () => {this.spinner.hide()}
        )
    }
  }

  public resetForm() : void
  {
    this.form.reset();
  }
}
