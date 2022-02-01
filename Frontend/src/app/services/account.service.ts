import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/Identity/User';
import { UserLogin } from '@app/Identity/UserLogin';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { BaseServiceService } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends BaseServiceService<User> {

  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = this.baseUrl + 'account';
  }

  public login(model: UserLogin): Observable<void> {
    return this.http
      .post<User>(this.baseUrl + '/loginAsync', model)
      .pipe(
        take(1),
        map((user: User) => {
          if (user) {
            localStorage.setItem('token', user.token);
          }
        })
      );
  }

  public logout(): void {
    if (this.tokenHeader) {
      localStorage.removeItem('token');
    }
  }

  public register(model: User): Observable<void> {
    return this.http
      .post<User>(this.baseUrl + '/registerAsync', model)
      .pipe(
        take(1),
        map((user: User) => {
        })
      );
  }

}
