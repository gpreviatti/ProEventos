import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormHelper } from '@app/helpers/FormHelper';
import { RouterHelper } from '@app/helpers/RouterHelper';
import { RedeSocial } from '@app/models/RedeSocial';
import { RedeSocialService } from '@app/services/rede-social.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-rede-social-detalhe',
  templateUrl: './rede-social-detalhe.component.html',
  styleUrls: ['./rede-social-detalhe.component.scss']
})
export class RedeSocialDetalheComponent implements OnInit {

  public palestranteId = 0;
  redeSocial = {} as RedeSocial;

  public formRedeSocial = this.formBuilderRedesSociais.group({
    nome : ['', [Validators.required, Validators.minLength(4), Validators.max(100)]],
    url: ['', [Validators.required]],
  });

  public formRedeSocialControls = this.formRedeSocial.controls;

  constructor(
    private redeSocialService: RedeSocialService,
    private formBuilderRedesSociais: FormBuilder,
    public formHelper: FormHelper,
    public bsModalRef: BsModalRef,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private routerHelper: RouterHelper,
  ) { }

  ngOnInit(): void {
    this.detalhar();
  }

  public detalhar(): void {
    if (this.redeSocial) {
      this.formRedeSocial.patchValue(this.redeSocial);
    }
  }

  public cadastrarAlterar(): void {
    this.spinner.show();
    if (this.formRedeSocial.invalid || this.palestranteId == null) {
      this.toastr.error('Invalid form');
      this.spinner.hide();
      return;
    }

    const redeSocial = {...this.formRedeSocial.value} as RedeSocial;
    redeSocial.palestranteId = this.palestranteId;

    if (this.redeSocial.id !== undefined) {
      redeSocial.id = this.redeSocial.id;
    }

    this.redeSocialService.post(redeSocial).subscribe(
      (redeSocialResponse: RedeSocial) => {
        if (redeSocialResponse) {
          this.toastr.success('Rede social criada com sucesso', 'Sucesso!');
          this.bsModalRef.hide();
          this.routerHelper.reloadComponent(`/palestrantes/detalhe/${this.palestranteId}`);
        }
      },
      (error: any) => this.toastr.error(error.message, 'Error!')
    ).add(() => this.spinner.hide());
  }

  public deletar(): void {
    this.spinner.show();
    this.redeSocialService.delete(this.redeSocial.id).subscribe(
      (response: boolean) => {
        if (response) {
          this.toastr.success('Rede social removida com sucesso', 'Sucesso!');
          this.bsModalRef.hide();
          this.routerHelper.reloadComponent(`/palestrantes/detalhe/${this.palestranteId}`);
        }
      },
      (error: any) => this.toastr.error(error.message, 'Error!')
    ).add(() => this.spinner.hide());
  }

}
