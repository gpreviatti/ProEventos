import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from '@app/helpers/FormHelper';
import { User } from '@app/Identity/User';
import { AccountService } from '@app/services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public user = {} as User;

  formOptions: AbstractControlOptions = {
    validators: this.formHelper.mustMatch('password', 'confirmPassword')
  };

  form = this.formBuilder.group({
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required ]],
    confirmPassword: ['', [Validators.required ]]
  }, this.formOptions);

  formControls = this.form.controls;

  constructor(
    private formBuilder: FormBuilder,
    public formHelper: FormHelper,
    public accountService: AccountService,
    public router: Router,
    public toaster: ToastrService
  ) { }

  ngOnInit(): void {
  }

  public resetForm(): void {
    this.form.reset();
  }

}
