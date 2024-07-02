import { Component } from '@angular/core';
import { FilterReservationUserDTO } from 'src/app/Models/DTO/FiltersDTO';
import { ReservationUser } from 'src/app/Models/DTO/ReservationsDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-my-reservations',
  templateUrl: './my-reservations.component.html',
  styleUrls: ['./my-reservations.component.css']
})
export class MyReservationsComponent {

  reservations:ReservationUser[] = []
  commonSpaces:CommonSpacesModel[] = [] 

  //FILTERS
  selectedCommomSpace:number = 0;
  dateFrom:string = "";
  dateTo:string = "";

  constructor(private reservationService:ReservationsService){
    
  }

  ngOnInit(){
    this.LoadReservationsByUser();
    this.reservationService.GetCommonSpacesByUser().subscribe((data)=>{
      this.commonSpaces = data
    })
  }

  LoadReservationsByUser(){
    var filter = new FilterReservationUserDTO();
    this.reservationService.GetReservationsByUserID(filter).subscribe((data)=>{
      this.reservations = data
    })
  }

  CancelReservation(reservationUser:ReservationUser){
    this.reservationService.CancelReservation(reservationUser.id).subscribe((data)=>{
      console.log(data);
      this.LoadReservationsByUser()
    })
  }
  Search(){
    var filter = new FilterReservationUserDTO();
    console.log(this.selectedCommomSpace);
    
    filter.commonSpaceID =  this.selectedCommomSpace;
    filter.dateFrom = this.dateFrom;
    filter.dateTo = this.dateTo;
    this.reservationService.GetReservationsByUserID(filter).subscribe((data)=>{
      this.reservations = data
    })
  }
}
