import { Utils } from "src/app/Helpers/Utils";

export class BaseFilter {
  dateFrom?: string;
  dateTo?: string = Utils.parseDate(new Date());
}

//CLAIMS
export class BaseFilterClaimDTO extends BaseFilter {
  causeClaim: number = 0;
  nroReclamo?: string = '';
}

export class FilterClaimDTO extends BaseFilterClaimDTO {
  stateID: number = 0;
}

export class FilterClaimUserDTO extends BaseFilterClaimDTO {
}


//RESERVATIONS
export class BaseFilterReservationsDTO extends BaseFilter {
  commonSpaceID: number = 0;
}

export class FilterReservationDTO extends BaseFilterReservationsDTO {
  document: number = 0;
}

export class FilterReservationUserDTO extends BaseFilterReservationsDTO {
}





