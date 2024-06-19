import { Utils } from "src/app/Helpers/Utils";

export class BaseFilterClaimDTO {
  causeClaim: number = 0;
  nroReclamo?: string;
  dateFrom?: string;
  dateTo: string = Utils.parseDate(new Date());
}

export class FilterClaimDTO extends BaseFilterClaimDTO {
  stateID: number;

  constructor() {
      super();
      this.stateID = 0;
  }
}

export class FilterClaimUserDTO extends BaseFilterClaimDTO {

}
