import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Utils } from 'src/app/Helpers/Utils';
import { UpdateStateReservationDTO } from 'src/app/Models/DTO/ReservationsDTO';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-cancel-reservation',
  templateUrl: './cancel-reservation.component.html',
  styleUrls: ['./cancel-reservation.component.css']
})
export class CancelReservationComponent {
  @Output() _ShowModal = new EventEmitter<boolean>();
  @Input() Reservation:any;

  message:string = "";
  loading:boolean = false;

  constructor(private reservationService:ReservationsService){}


  CloseModal(){
    this._ShowModal.emit(false);
  }


  CancelReservation() {
    var dto = new UpdateStateReservationDTO();
    dto.reservationID = this.Reservation.id;
    dto.stateReservationID = 2;
    dto.message = this.message
    this.loading = true;
    this.reservationService.UpdateStateReservation(dto).subscribe({
      next: () => {
        this._ShowModal.emit(false);
        this.loading = false;
        Utils.success("Se cancelo con exito la reserva")
      },
      error: () => {
        this.loading = false;
        Utils.error("Error al cancelar la reserva")
      }
    
    })
  }
}
