import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-reservation-common-spaces',
  templateUrl: './reservation-common-spaces.component.html',
  styleUrls: ['./reservation-common-spaces.component.css']
})
export class ReservationCommonSpacesComponent implements OnInit{
  commonSpaces:CommonSpacesModel[] = [];

  constructor(private reservationService:ReservationsService,
    private router:Router
  ) 
  { 
  }

  ngOnInit(): void {
    this.reservationService.GetCommonSpacesByUser().subscribe((data)=>{
      console.log(data);
      this.commonSpaces = data;
    })
  }

  
	ReserveSpace(space:CommonSpacesModel){
    this.router.navigate(['/main-page-user/reservation-user',space.id])
	}
}
