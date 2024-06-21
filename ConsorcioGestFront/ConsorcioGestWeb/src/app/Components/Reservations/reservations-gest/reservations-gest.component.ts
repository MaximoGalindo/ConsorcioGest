import { Component } from '@angular/core';
import { FilterReservationDTO } from 'src/app/Models/DTO/FiltersDTO';
import { UpdateStateReservationDTO } from 'src/app/Models/DTO/ReservationsDTO';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ReservationsService } from 'src/app/Services/reservations.service';

@Component({
  selector: 'app-reservations-gest',
  templateUrl: './reservations-gest.component.html',
  styleUrls: ['./reservations-gest.component.css']
})
export class ReservationsGestComponent {

  _ShowGrid: Boolean = false;
  _ShowCancelReservation: Boolean = false;

  commonSpaces: CommonSpacesModel[] = []
  reservations: any[] = []
  selectedReservation: any;

  //FILTERS
  selectedCommonSpace: number = 0
  dateFrom: string = "";
  dateTo: string = "";
  document: number = 0;

  constructor(private reservationService: ReservationsService) { }

  ngOnInit() {
    this.reservationService.GetCommonSpaces().subscribe((data) => {
      this.commonSpaces = data
    })
  }

  ShowGrid($event: Number) {
    this._ShowGrid = true
    var filter = new FilterReservationDTO();
    this.selectedCommonSpace = $event.valueOf();
    filter.commonSpaceID = $event.valueOf();
    this.reservationService.GetReservations(filter).subscribe((data) => {
      this.reservations = data;
    })
  }

  Back() {
    this._ShowGrid = false
  }

  Search() {
    var filter = new FilterReservationDTO();
    filter.commonSpaceID = this.selectedCommonSpace;
    filter.document = this.document;
    filter.dateFrom = this.dateFrom;
    filter.dateTo = this.dateTo;

    this.reservationService.GetReservations(filter).subscribe((data) => {
      this.reservations = data;
    })
  }

  CloseModal() {
    this._ShowCancelReservation = false
  }

  CancelReservation(reservation: any) {
    this._ShowCancelReservation = true
    this.selectedReservation = reservation
  }

}
