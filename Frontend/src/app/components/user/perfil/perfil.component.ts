import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from '@app/helpers/FormHelper';
import { User } from '@app/Identity/User';
import { UserUpdate } from '@app/Identity/UserUpdate';
import { AccountService } from '@app/services/account.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public user = {} as UserUpdate;

  form = this.formBuilder.group({
    userName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    email: ['', [Validators.required, Validators.email]]
  });

  formControls = this.form.controls;

  constructor(
    private formBuilder: FormBuilder,
    public formHelper: FormHelper,
    public accountService: AccountService,
    public router: Router,
    public toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getUser();
  }

  public resetForm(): void {
    this.form.reset();
  }

  public getUser(): void {
    this.form.patchValue(this.accountService.getUser());
  }

  public updateUser(): void {
    this.spinner.show();
    this.accountService.update(this.form.value).subscribe(
      (user: UserUpdate) => {
        this.user = user;
        this.accountService.setUser(user);
        this.toaster.success('Perfil atualizado com sucesso!');
      },
      (error: any) =>  this.toaster.error(error.message),
      () => this.spinner.hide()
    );
  }
}
