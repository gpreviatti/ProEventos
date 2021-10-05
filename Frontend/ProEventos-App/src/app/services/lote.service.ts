import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { Observable } from 'rxjs';

@Injectable()
export class LoteService {

  baseUrl: string = 'https://localhost:5001/lotes';

  constructor(private http: HttpClient) { }

  public getByEventoById(eventoId : number) : Observable<Lote[]>
  {
    return this.http.get<Lote[]>(`${this.baseUrl}/${eventoId}`);
  }

  public put(eventoId : number, lotes : Lote[]) : Observable<Lote[]>
  {
    return this.http.put<Lote[]>(`${this.baseUrl}/${eventoId}`, lotes);
  }

  public delete(eventoId : number, id : number) : Observable<boolean>
  {
    return this.http.delete<boolean>(`${this.baseUrl}/${eventoId}/${id}`);
  }
}
