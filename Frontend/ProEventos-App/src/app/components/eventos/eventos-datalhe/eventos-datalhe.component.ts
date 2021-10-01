import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-eventos-datalhe',
  templateUrl: './eventos-datalhe.component.html',
  styleUrls: ['./eventos-datalhe.component.scss']
})
export class EventosDatalheComponent implements OnInit
{

  form : FormGroup = this.formBuilder.group({
    tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
    local: ['', Validators.required, ],
    dataEvento: ['', Validators.required, ],
    qtdPessoas: ['', [Validators.required, Validators.maxLength(120000)]],
    telefone: ['', Validators.required ],
    email: ['', [Validators.required, Validators.email]],
    imagemURL: ['', Validators.required ]
  });

  formControls = this.form.controls;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

}
