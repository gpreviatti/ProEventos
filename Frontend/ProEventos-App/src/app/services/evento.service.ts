import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable()
export class EventoService {

  protected baseUrl = environment.apiUrl + 'eventos';

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

  public delete(id : number) : Observable<boolean>
  {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }

  public postUpload(eventoId : Number, file : any) : Observable<Evento>
  {
    const fileToUpload = file[0] as File;
    let formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Evento>(`${this.baseUrl}/upload-image/${eventoId}`, formData);
  }
}
