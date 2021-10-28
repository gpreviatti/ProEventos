import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseServiceService<Type> {

  protected baseUrl = environment.apiUrl;

  constructor(protected http: HttpClient) { }

  public get(): Observable<Type[]> {
    return this.http.get<Type[]>(this.baseUrl);
  }

  public getById(id: number): Observable<Type> {
    return this.http.get<Type>(`${this.baseUrl}/${id}`);
  }

  public post(entity: Type): Observable<Type> {
    return this.http.post<Type>(this.baseUrl, entity);
  }

  public delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
