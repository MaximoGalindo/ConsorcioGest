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

export class FilterReservationDTO {
  dateFrom?: string;
  dateTo?: string;
  document: number = 0;
  commonSpaceID: number = 0;
}

export class FilterReservationUserDTO {
  dateFrom?: string;
  dateTo?: string;
  commonSpaceID: number = 0;
}

export class FilterUserDTO{

  document: number = 0;
  tower:string = '';
  codominium:string = '';
  userStateID: number = 0;
}

export class FilterSurveyDTO extends BaseFilter {
  stateID: number = 0;
  claimNumber: string = '';
}




