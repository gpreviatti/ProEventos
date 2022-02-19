import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserLogin } from '@app/Identity/UserLogin';
import { AccountService } from '@app/services/account.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public lock = true;
  public model = {} as UserLogin;

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {

  }

  public login() {
    this.spinner.show();
    this.accountService.login(this.model)
    .subscribe(
      () => { this.router.navigateByUrl('/eventos'); },
      (error: any) => {
        if (error.status === 401) {
            this.toaster.error('Usuário ou senha inválidos.');
        } else {
          this.toaster.error(error.message);
        }
      }
    ).add(() => this.spinner.hide());
  }

}
