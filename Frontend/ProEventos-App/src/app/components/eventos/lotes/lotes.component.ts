import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoteService } from '@app/services/lote.service';

@Component({
  selector: 'app-lotes',
  templateUrl: './lotes.component.html',
  styleUrls: ['./lotes.component.scss']
})
export class LotesComponent implements OnInit {

  formLotes = this.formBuilder.group({
    nome: '',
    quantidade: '',
    preco: '',
    dataInicio: '',
    dataFim: ''
  });

  public formControls = this.formLotes.controls;

  constructor(
    private formBuilder: FormBuilder,
    private loteService: LoteService
  ) { }

  @Input() eventoId = 0;
  ngOnInit(): void
  {
    console.log(this.eventoId)
  }

}
