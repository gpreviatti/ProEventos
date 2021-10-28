import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Palestrante } from '@app/models/Palestrante';
import { BaseServiceService } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class PalestranteService extends BaseServiceService<Palestrante> {

  constructor(http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'palestrante';
  }
}
