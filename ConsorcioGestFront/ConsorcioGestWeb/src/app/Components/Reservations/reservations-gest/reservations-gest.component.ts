import { Component } from '@angular/core';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-reservations-gest',
  templateUrl: './reservations-gest.component.html',
  styleUrls: ['./reservations-gest.component.css']
})
export class ReservationsGestComponent {


  _ShowGrid:Boolean = false;

  commonSpaces:CommonSpacesModel[] = []
  reservations:any[] = []

  constructor(private reservationService:ReservationsService){}

  ngOnInit(){
    this.reservationService.GetCommonSpaces().subscribe((data)=>{    
      this.commonSpaces = data
    })
  }

  ShowGrid($event: Number) {
    this._ShowGrid = true
    this.reservationService.GetReservations($event).subscribe((data) => {
      console.log(data);
      this.reservations = data;
    })
  }

  Back(){
    this._ShowGrid = false
  }

}
