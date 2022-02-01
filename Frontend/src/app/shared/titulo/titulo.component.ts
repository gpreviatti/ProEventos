import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

  constructor(private router: Router) { }

  @Input() iconClass = 'fa fa-user';
  @Input() titulo = '';
  @Input() subTitulo = '';
  @Input() botaoListar = false;
  ngOnInit(): void
  {
  }

  public listar(): void
  {
    this.router.navigate([`/${this.titulo.toLowerCase()}/lista`]);

  }

}
