import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedRequest } from '@app/messages/PaginatedRequest';
import { PaginatedResponse } from '@app/messages/PaginatedResponse';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BaseServiceService<T> {

  protected tokenHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('token') });
  protected baseUrl = environment.apiUrl;

  constructor(protected http: HttpClient) { }

  public get(): Observable<T[]> {
    return this.http.get<T[]>(this.baseUrl, { headers: this.tokenHeader });
  }

  public getPaginated(paginatedRequest: PaginatedRequest): Observable<PaginatedResponse<T[]>> {
    let params = new HttpParams;

    if (paginatedRequest.currentPage !== null && paginatedRequest.pageSize !== null) {
      params = params.append('currentPage', paginatedRequest.currentPage);
      params = params.append('pageSize', paginatedRequest.pageSize);
    }

    if (paginatedRequest.searchValue !== '' && paginatedRequest.searchValue !== undefined) {
      params = params.append('searchValue', paginatedRequest.searchValue);
    }

    return this.http
      .get<PaginatedResponse<T[]>>(
        `${this.baseUrl}/paginated`,
        {observe: 'response', params, headers: this.tokenHeader}
      )
      .pipe(
        take(1),
        map(response => {
          return {
            data: response.body?.data as T[],
            currentPage: response.body?.currentPage as number,
            pageSize: response.body?.pageSize as number,
            recordsTotal: response.body?.recordsTotal as number,
            recordsFiltered: response.body?.recordsTotal as number,
            totalPages: response.body?.totalPages as number
          } as PaginatedResponse<T[]>;
        })
      );
  }

  public getById(id: number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${id}`, { headers: this.tokenHeader });
  }

  public post(entity: T): Observable<T> {
    return this.http.post<T>(this.baseUrl, entity, { headers: this.tokenHeader });
  }

  public delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`, { headers: this.tokenHeader });
  }
}
