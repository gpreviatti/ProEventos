import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedRequest } from '@app/messages/PaginatedRequest';
import { PaginatedResponse } from '@app/messages/PaginatedResponse';
import { Palestrante } from '@app/models/Palestrante';
import { PalestranteService } from '@app/services/palestrante.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-palestrante-lista',
  templateUrl: './palestrante-lista.component.html',
  styleUrls: ['./palestrante-lista.component.scss']
})
export class PalestranteListaComponent implements OnInit {

  public palestrantes = [] as Palestrante[];
  public paginatedRequest = {} as PaginatedRequest;
  public searchValueChanged: Subject<string> = new Subject<string>();

  constructor(
    private palestranteService: PalestranteService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.paginatedRequest = {
      currentPage: 1,
      pageSize: 10,
      totalItems: 100,
      totalPages: 1
    };

    this.getPalestrantes();
  }

  public getPalestrantes(): any {
    this.spinner.show();
    this.palestranteService
    .getPaginated(this.paginatedRequest)
    .subscribe(
      (response: PaginatedResponse<Palestrante[]>) => {
        this.palestrantes = response.data;

        this.paginatedRequest = {
          currentPage: response.currentPage,
          pageSize: response.pageSize,
          totalItems: response.recordsTotal,
          totalPages: response.recordsTotal
        } as PaginatedRequest;

      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error(error.message, 'Erro!');
      },
      () => this.spinner.hide()
    );
  }

  public pageChanged(event: any): void {
    if (this.searchValueChanged.observers.length === 0) {
      this.searchValueChanged.pipe(debounceTime(500))
      .subscribe(
        (filter) => {
          this.paginatedRequest.searchValue = filter;
          this.getPalestrantes();
        }
      );
    }
    this.searchValueChanged.next(event.value);
  }

  public detalhePalestrante(palestranteId: number): void {
    this.router.navigate([`/palestrantes/detalhe/${palestranteId}`]);
  }
}
