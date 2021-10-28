import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RedeSocial } from '@app/models/RedeSocial';
import { Observable } from 'rxjs';
import { BaseServiceService } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class RedeSocialService extends BaseServiceService<RedeSocial> {

  constructor(http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'redeSocial';
  }

  public getByPalestranteId(palestranteId: number): Observable<RedeSocial[]> {
    return this.http.get<RedeSocial[]>(`${this.baseUrl}/palestrante/${palestranteId}`);
  }
}
