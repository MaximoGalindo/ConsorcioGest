import { Component, Input } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';

@Component({
  selector: 'app-claims-info',
  templateUrl: './claims-info.component.html',
  styleUrls: ['./claims-info.component.css']
})
export class ClaimsInfoComponent {

  @Input() Claim:ClaimDTO = new ClaimDTO();
  _ShowModal:boolean = false;
  IsAdmin:boolean = false;
  ngOnInit(){
  }


  VerMas(){
    this._ShowModal = true;
  }

  CloseModal(){
    this._ShowModal = false;
  }
}
