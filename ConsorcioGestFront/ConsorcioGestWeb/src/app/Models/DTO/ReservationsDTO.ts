export class ReservationDTO {
  date: string = ''
  hourFrom: string = '';
  hourTo: string = '';
  commonSpaceConsortiumID: number = 0;
}

export class ReservationUser {
  id: number = 0;
  hourFrom: string = '';
  hourTo: string = '';
  date: Date = new Date();
  stateReservationID: number = 0;
  stateReservation: string = '';
  commonSpaceConsortiumID: number = 0;
  commonSpaceConsortium: string = '';
}

export class UpdateStateReservationDTO {
  reservationID: number = 0;
  stateReservationID: number = 0;
  message: string = '';
}
