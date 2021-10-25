import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Palestrante } from '@app/models/Palestrante';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PalestranteService {

  protected baseUrl = environment.apiUrl + 'palestrante';

  constructor(private http: HttpClient) { }

  public get(): Observable<Palestrante[]> {
    return this.http.get<Palestrante[]>(this.baseUrl);
  }

  public getById(id: number): Observable<Palestrante> {
    return this.http.get<Palestrante>(`${this.baseUrl}/${id}`);
  }

  public post(palestrante: Palestrante): Observable<Palestrante> {
    return this.http.post<Palestrante>(this.baseUrl, palestrante);
  }

  public delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
