import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';

@Component({
  selector: 'app-common-space',
  templateUrl: './common-space.component.html',
  styleUrls: ['./common-space.component.css']
})
export class CommonSpaceComponent {
  
  @Output() _ShowGrid =  new EventEmitter<Number>();
  @Input() commonSpace:CommonSpacesModel = new CommonSpacesModel(); 
  countReservations:number = 0

  ShowGrid(commonSpaceID:number){
    this._ShowGrid.emit(commonSpaceID);
  }
}
