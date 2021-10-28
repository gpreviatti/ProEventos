import { Component, Input, OnInit } from '@angular/core';
import { RedeSocial } from '@app/models/RedeSocial';
import { RedeSocialService } from '@app/services/rede-social.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-rede-social',
  templateUrl: './rede-social.component.html',
  styleUrls: ['./rede-social.component.scss']
})
export class RedeSocialComponent implements OnInit {
  @Input() palestranteId = 0;
  redesSociais = [] as RedeSocial[];

  constructor(
    private redeSocialService: RedeSocialService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    console.log(this.palestranteId);
    this.carregarRedesSociais();
  }

  carregarRedesSociais(): any {
    this.spinner.show();
    this.redeSocialService.getByPalestranteId(this.palestranteId).subscribe(
      (redesSociais: RedeSocial[]) => this.redesSociais = redesSociais,
      (error: any) => this.toastr.error(error.message, 'Erro!')
    ).add(() => this.spinner.hide());
  }

  detalhar(redeSocial: RedeSocial): any {
    console.log(redeSocial);
  }

}
