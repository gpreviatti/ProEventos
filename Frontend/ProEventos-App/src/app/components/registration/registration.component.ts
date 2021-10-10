import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControlOptions, FormBuilder, Validators } from '@angular/forms';
import { FormHelper } from '@app/helpers/FormHelper';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit
{
  modalRef?: BsModalRef;

  formOptions : AbstractControlOptions = {
    validators: this.formHelper.mustMatch('password', 'confirmPassword')
  };

  form = this.formBuilder.group({
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    email: ['', [Validators.required, Validators.email]],
    userName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    password: ['', [Validators.required ]],
    confirmPassword: ['', [Validators.required ]]
  }, this.formOptions);

  public formControls = this.form.controls;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    public formHelper: FormHelper,
  ) { }

  ngOnInit(): void
  {

  }

  public openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
  }

}
