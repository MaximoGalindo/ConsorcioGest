import { Component } from '@angular/core';
import { ReservationUser } from 'src/app/Models/DTO/ReservationsDTO';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-my-reservations',
  templateUrl: './my-reservations.component.html',
  styleUrls: ['./my-reservations.component.css']
})
export class MyReservationsComponent {

  reservations:ReservationUser[] = []

  constructor(private reservationService:ReservationsService){
    
  }

  ngOnInit(){
    this.reservationService.GetReservationsByUserID().subscribe((data)=>{
      this.reservations = data
    })
  }
}
