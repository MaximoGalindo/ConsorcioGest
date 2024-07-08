import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-common-space',
  templateUrl: './common-space.component.html',
  styleUrls: ['./common-space.component.css']
})
export class CommonSpaceComponent {
  
  @Output() _ShowGrid =  new EventEmitter<Number>();
  @Input() commonSpace:CommonSpacesModel = new CommonSpacesModel(); 
  countReservations:number = 0

  constructor(private reservationService:ReservationsService) { }

  ShowGrid(commonSpaceID:number){
    this._ShowGrid.emit(commonSpaceID);
  }

  UpdateCommonSpace(commonSpaceID:number,state:boolean){
    console.log(state);
    console.log(commonSpaceID);
    
    
    this.reservationService.UpdateStateCommonSpace(commonSpaceID,state).subscribe((data)=>{
      window.location.reload();
    })
  }

}
