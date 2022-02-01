import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, Validators } from '@angular/forms';
import { FormHelper } from '@app/helpers/FormHelper';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

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
    public formHelper: FormHelper
  ) { }

  ngOnInit(): void {
  }

  public resetForm(): void {
    this.form.reset();
  }

}
