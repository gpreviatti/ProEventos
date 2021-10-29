import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';
import { BaseServiceService } from './base-service.service';

@Injectable()
export class EventoService extends BaseServiceService<Evento> {

  constructor(http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'eventos';
  }

  public getByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseUrl}/${tema}/tema`);
  }

  public postUpload(eventoId: Number, file: any): Observable<Evento> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Evento>(`${this.baseUrl}/upload-image/${eventoId}`, formData);
  }
}
