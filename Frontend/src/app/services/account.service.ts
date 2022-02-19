import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/Identity/User';
import { UserLogin } from '@app/Identity/UserLogin';
import { UserUpdate } from '@app/Identity/UserUpdate';
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

  public setUser(user: User) {
    localStorage.setItem('token', user.token);
    localStorage.setItem('userName', user.userName);
    localStorage.setItem('firstName', user.firstName);
    localStorage.setItem('lastName', user.lastName);
    localStorage.setItem('email', user.email);
  }

  public login(model: UserLogin): Observable<void> {
    return this.http
      .post<User>(this.baseUrl + '/loginAsync', model)
      .pipe(
        take(1),
        map((user: User) => {
          if (user) {
            this.setUser(user);
          }
        })
      );
  }

  public getUser(): User {
    const token = localStorage.getItem('token');
    if (token) {
      const user: User = {
        token: token,
        userName: localStorage.getItem('userName') as string,
        email: localStorage.getItem('email') as string,
        firstName: localStorage.getItem('firstName') as string,
        lastName: localStorage.getItem('lastName') as string,
        password: ''
      };
      return user;
    }
    return {} as User;
  }

  public logout(): void {
    if (this.tokenHeader) {
      localStorage.clear();
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

  public update(user: UserUpdate): Observable<UserUpdate> {
    return this.http.put<UserUpdate>(this.baseUrl + '/UpdateAsync', user, { headers: this.tokenHeader });
  }

}
