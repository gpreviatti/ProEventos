import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable()
export class EventoService {

  baseUrl: string = 'https://localhost:5001/eventos';

  constructor(private http: HttpClient) { }

  public get() : Observable<Evento[]>
  {
    return this.http.get<Evento[]>(this.baseUrl)
  }

  public getById(id : number) : Observable<Evento>
  {
    return this.http.get<Evento>(`${this.baseUrl}/${id}`);
  }

  public getByTema(tema : string) : Observable<Evento[]>
  {
    return this.http.get<Evento[]>(`${this.baseUrl}/${tema}/tema`);
  }

  public post(evento : Evento) : Observable<Evento>
  {
    return this.http.post<Evento>(`${this.baseUrl}`, evento);
  }

  public put(evento : Evento) : Observable<Evento>
  {
    return this.http.put<Evento>(`${this.baseUrl}/${evento.id}`, evento);
  }

  public delete(id : number) : Observable<string>
  {
    return this.http.delete<string>(`${this.baseUrl}/${id}`);
  }
}
