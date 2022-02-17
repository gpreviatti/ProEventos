import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '@app/Identity/User';
import { AccountService } from '@app/services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  public isCollapsed = true;

  constructor(
    private router: Router,
    private accountService: AccountService
  ) { }

  ngOnInit(): void { }

  public showMenu(): boolean {
    return this.router.url !== '/login' &&
           this.router.url !== '/registration' &&
           this.router.url !== '/notfound';
  }

  public logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }

  public getUser(): User {
    return this.accountService.getUser();
  }

}
