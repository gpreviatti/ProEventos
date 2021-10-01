import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit
{
  modalRef?: BsModalRef;

  form = this.formBuilder.group({
    firstName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    lastName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    email: ['', [Validators.required, Validators.email]],
    userName: ['', [Validators.required, Validators.minLength(4), Validators.max(120000)]],
    password: ['', [Validators.required, ]],
    confirmPassword: ['', [Validators.required ]]
  });

  formControls = this.form.controls;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void
  {

  }

  public openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
  }

}
