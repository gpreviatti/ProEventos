import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class LoteService {

  baseUrl: string = environment.apiUrl + 'lotes';

  constructor(private http: HttpClient) { }

  public getByEventoById(eventoId: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseUrl}/${eventoId}`);
  }

  public getByEventoByIdAndLoteId(eventoId: number, loteId: number): Observable<Lote> {
    return this.http.get<Lote>(`${this.baseUrl}/${eventoId}/${loteId}`);
  }

  public put(eventoId: number, lote: Lote): Observable<Lote> {
    return this.http.put<Lote>(`${this.baseUrl}/${eventoId}`, lote);
  }

  public delete(eventoId: number, id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${eventoId}/${id}`);
  }
}
