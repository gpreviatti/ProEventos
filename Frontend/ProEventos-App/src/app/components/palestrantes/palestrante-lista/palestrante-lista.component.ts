import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Palestrante } from '@app/models/Palestrante';
import { PalestranteService } from '@app/services/palestrante.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-palestrante-lista',
  templateUrl: './palestrante-lista.component.html',
  styleUrls: ['./palestrante-lista.component.scss']
})
export class PalestranteListaComponent implements OnInit {

  palestrantes = [] as Palestrante[];

  constructor(
    private palestranteService: PalestranteService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getPalestrantes();
  }

  public getPalestrantes(): any {
    this.spinner.show();
    this.palestranteService
      .get()
      .subscribe({
        next: (palestrantes: Palestrante[]) => {
          this.palestrantes = palestrantes;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error(error.message, 'Erro!');
        },
        complete: () => this.spinner.hide()
      });
  }

  public detalhePalestrante(palestranteId: number): void {
    this.router.navigate([`/palestrantes/detalhe/${palestranteId}`]);
  }
}
