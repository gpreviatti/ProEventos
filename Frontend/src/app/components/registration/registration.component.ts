import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControlOptions, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from '@app/helpers/FormHelper';
import { User } from '@app/Identity/User';
import { AccountService } from '@app/services/account.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  bsModalRef?: BsModalRef;

  public user = {} as User;

  formOptions: AbstractControlOptions = {
    validators: this.formHelper.mustMatch('password', 'confirmPassword')
  };

  form = this.formBuilder.group({
    userName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    email: ['', [Validators.required, Validators.email]],
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    password: ['', [Validators.required ]],
    confirmPassword: ['', [Validators.required ]]
  }, this.formOptions);

  public formControls = this.form.controls;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    public formHelper: FormHelper,
    public accountService: AccountService,
    private router: Router,
    private toaster: ToastrService
  ) { }

  ngOnInit(): void {

  }

  public openModal(template: TemplateRef<any>): void {
    this.bsModalRef = this.modalService.show(template, { class: 'modal-lg' });
  }

  public register(): void {
    this.user = { ...this.form.value };
    this.accountService.register(this.user).subscribe(
      () => {
        this.toaster.success('Usuário cadastrado com sucesso.');
        this.router.navigateByUrl('/login');
      },
      (error: any) => {this.toaster.error(error.message); }
    );
  }

}
