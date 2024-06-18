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
    this.LoadReservationsByUser();
  }

  LoadReservationsByUser(){
    this.reservationService.GetReservationsByUserID().subscribe((data)=>{
      this.reservations = data
    })
  }

  CancelReservation(reservationUser:ReservationUser){
    var dto = {
      ReservationID: reservationUser.id,
      StateReservationID: 2
    }
    this.reservationService.CancelReservation(dto).subscribe((data)=>{
      console.log(data);
      this.LoadReservationsByUser()
    })
  }
}
