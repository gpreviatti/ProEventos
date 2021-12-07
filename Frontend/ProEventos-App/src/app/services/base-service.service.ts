import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedRequest } from '@app/messages/PaginatedRequest';
import { PaginatedResponse } from '@app/messages/PaginatedResponse';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BaseServiceService<Type> {

  protected baseUrl = environment.apiUrl;

  constructor(protected http: HttpClient) { }

  public get(): Observable<Type[]> {
    return this.http.get<Type[]>(this.baseUrl);
  }

  public getPaginated(paginatedRequest: PaginatedRequest): Observable<PaginatedResponse<Type[]>> {
    let params = new HttpParams;

    if (paginatedRequest.currentPage !== null && paginatedRequest.pageSize !== null) {
      params = params.append('currentPage', paginatedRequest.currentPage);
      params = params.append('pageSize', paginatedRequest.pageSize);
    }

    if (paginatedRequest.searchValue !== '' && paginatedRequest.searchValue !== undefined) {
      params = params.append('searchValue', paginatedRequest.searchValue);
    }

    return this.http
      .get<PaginatedResponse<Type[]>>(`${this.baseUrl}/paginated`, {observe: 'response', params})
      .pipe(
        take(1),
        map(response => {
          return {
            data: response.body?.data as Type[],
            currentPage: response.body?.currentPage as number,
            pageSize: response.body?.pageSize as number,
            recordsTotal: response.body?.recordsTotal as number,
            recordsFiltered: response.body?.recordsTotal as number,
            totalPages: response.body?.totalPages as number
          } as PaginatedResponse<Type[]>;
        })
      );
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
