import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { Observable } from 'rxjs';
import { BaseServiceService } from './base-service.service';

@Injectable()
export class LoteService extends BaseServiceService<Lote> {

  constructor(http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'lotes';
  }

  public getByEventoById(eventoId: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseUrl}/${eventoId}`);
  }

  public getByEventoByIdAndLoteId(eventoId: number, loteId: number): Observable<Lote> {
    return this.http.get<Lote>(`${this.baseUrl}/${eventoId}/${loteId}`);
  }
}
