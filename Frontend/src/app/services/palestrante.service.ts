import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Palestrante } from '@app/models/Palestrante';
import { Observable } from 'rxjs';
import { BaseServiceService } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class PalestranteService extends BaseServiceService<Palestrante> {

  constructor(http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'palestrantes';
  }

  public uploadImage(id: Number, file: any): Observable<Palestrante> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Palestrante>(`${this.baseUrl}/upload-image/${id}`, formData, { headers: this.tokenHeader });
  }
}
