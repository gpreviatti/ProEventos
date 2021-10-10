import { Injectable } from "@angular/core";
import { AbstractControl, FormGroup } from "@angular/forms";

@Injectable()
export class FormHelper
{
  //#region Custom Validators
  public mustMatch(controlName: string, matchingControlName: string): any
  {
    return (group: AbstractControl) => {
      const formGroup = group as FormGroup;
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && matchingControl.errors.mustMatch) {
        return null;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true});
      } else
      {
        matchingControl.setErrors(null);
      }

      return null;
    }
  }
  //#endregion

  public resetForm(formGroup: FormGroup): void
  {
    formGroup.reset();
  }

  public cssValidator(campo : AbstractControl) : any
  {
    return {'is-invalid' : campo.errors && campo.touched}
  }
}
