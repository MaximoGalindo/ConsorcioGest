import { Component } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';
import { ClaimsCountByStatesDTO } from 'src/app/Models/DTO/ClaimsCountByStatesDTO';
import { FilterClaimDTO } from 'src/app/Models/DTO/FilterClaimDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { ClaimService } from 'src/app/Services/claim.service';

@Component({
  selector: 'app-claims-gest',
  templateUrl: './claims-gest.component.html',
  styleUrls: ['./claims-gest.component.css']
})
export class ClaimsGestComponent {
  ClaimsList:ClaimDTO[] = [];
  idState:number = 0;
  statesCount: ClaimsCountByStatesDTO[] = [];
  totalClaims: number = 0;
  selectedState:number = 0;
  causeClaims:ListItemDTO[] = [];

  //FILTERS
  selectedCauseClaim:ListItemDTO = new ListItemDTO();
  dateFrom:string = "";
  dateTo:string = "";
  claimNumber:string = "";


  constructor(private claimService:ClaimService){

  }

  ChangeState(idState:number){
    var filter = new FilterClaimDTO();
    filter.stateID = idState;
    this.claimService.GetAllClaims(filter).subscribe((data)=>{     
      this.ClaimsList = data;
      this.selectedState = idState
    })
  }
  ngOnInit(): void {

    var filter = new FilterClaimDTO();
    filter.stateID = 0;

    this.GetAllClaims(filter);
    this.GetCountClaimsByState();
    this.claimService.GetCauseClaim().subscribe((data)=>{
      this.causeClaims = data;
    })
  }

  GetAllClaims(filter:FilterClaimDTO){
    this.claimService.GetAllClaims(filter).subscribe((data)=>{      
      this.ClaimsList = data;
    })
  }

  GetCountClaimsByState(){
    this.claimService.GetCountClaimsByState().subscribe((data)=>{
      this.statesCount = data;
      this.totalClaims = this.statesCount.reduce((total, state) => total + state.count, 0);
    })
  }

  getStateText(stateID: number): string {
    if(stateID == 1) return 'Pendiente';
    if(stateID == 2) return 'En Proceso';
    if(stateID == 3) return 'Cancelado';
    if(stateID == 4) return 'Finalizado';
    return '';
  }

  Reload(){
    var filter = new FilterClaimDTO();
    filter.stateID = this.selectedState != null? this.selectedState : 0

    this.GetAllClaims(filter);
    this.GetCountClaimsByState();
  }

  Search(){   
    var filter = new FilterClaimDTO ();
    filter.stateID = this.selectedState != null ? this.selectedState : 0
    filter.causeClaim = this.selectedCauseClaim.id;
    filter.nroReclamo = this.claimNumber != '' ? this.claimNumber : '';
    filter.dateFrom = this.dateFrom != '' ? this.dateFrom : '';
    filter.dateTo = this.dateTo != '' ? this.dateTo : '';
    this.claimService.GetClaimsByUser(filter).subscribe((data)=>{
      this.ClaimsList = data;
    })
  }

}
