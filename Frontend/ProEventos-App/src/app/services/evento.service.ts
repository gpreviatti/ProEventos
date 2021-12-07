import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedRequest } from '@app/messages/PaginatedRequest';
import { PaginatedResponse } from '@app/messages/PaginatedResponse';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';
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

  public getPaginated(paginatedRequest: PaginatedRequest): Observable<PaginatedResponse<Evento[]>> {
    let params = new HttpParams;

    if (paginatedRequest.pageNumber !== null && paginatedRequest.pageSize !== null) {
      params = params.append('pageNumber', paginatedRequest.pageNumber);
      params = params.append('pageSize', paginatedRequest.pageSize);
    }

    return this.http
      .get<PaginatedResponse<Evento[]>>(`${this.baseUrl}/paginated`, {observe: 'response', params})
      .pipe(
        take(1),
        map(response => {
          return {
            data: response.body?.data as Evento[],
            pageNumber: response.body?.pageNumber as number,
            pageSize: response.body?.pageSize as number,
            recordsTotal: response.body?.recordsTotal as number,
            recordsFiltered: response.body?.recordsTotal as number,
            totalPages: response.body?.totalPages as number
          } as PaginatedResponse<Evento[]>;
        })
      );
  }

  public postUpload(eventoId: Number, file: any): Observable<Evento> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Evento>(`${this.baseUrl}/upload-image/${eventoId}`, formData);
  }
}
