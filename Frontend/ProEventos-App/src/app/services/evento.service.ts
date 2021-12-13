import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';
import { BaseServiceService } from './base-service.service';

@Injectable()
export class EventoService extends BaseServiceService<Evento> {

  constructor(http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'eventos';
  }

  public uploadImage(eventoId: Number, file: any): Observable<Evento> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Evento>(`${this.baseUrl}/upload-image/${eventoId}`, formData);
  }
}
