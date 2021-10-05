import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

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


  constructor(
    private formBuilder: FormBuilder,
    public validators: ValidatorField
  ) { }

  ngOnInit(): void { }

  public resetForm() : void
  {
    this.form.reset();
  }
}
