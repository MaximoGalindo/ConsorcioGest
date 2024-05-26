import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';

@Component({
  selector: 'app-claims-info',
  templateUrl: './claims-info.component.html',
  styleUrls: ['./claims-info.component.css']
})
export class ClaimsInfoComponent {

  @Output() _Reaload: EventEmitter<boolean> = new EventEmitter<boolean>(); 

  @Input() Claim:ClaimDTO = new ClaimDTO();
  _ShowModal:boolean = false;
  @Input() IsAdmin:boolean = false;
  ngOnInit(){
  }


  VerMas(){
    this._ShowModal = true;
  }

  CloseModal(){
    this._ShowModal = false;
  }

  Reaload(){
    this._Reaload.emit(true);
  }
}
