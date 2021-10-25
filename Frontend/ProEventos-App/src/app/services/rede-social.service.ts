import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RedeSocial } from '@app/models/RedeSocial';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RedeSocialService {

  protected baseUrl = environment.apiUrl + 'redeSocial';

  constructor(private http: HttpClient) { }

  public getByPalestranteId(palestranteId: number): Observable<RedeSocial[]> {
    return this.http.get<RedeSocial[]>(`${this.baseUrl}/palestrante/${palestranteId}`);
  }

  public getById(id: number): Observable<RedeSocial> {
    return this.http.get<RedeSocial>(`${this.baseUrl}/${id}`);
  }

  public post(redeSocial: RedeSocial): Observable<RedeSocial> {
    return this.http.post<RedeSocial>(this.baseUrl, redeSocial);
  }

  public delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
