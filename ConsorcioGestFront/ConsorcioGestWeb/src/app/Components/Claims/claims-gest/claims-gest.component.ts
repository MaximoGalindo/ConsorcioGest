import { Component } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';
import { ClaimsCountByStatesDTO } from 'src/app/Models/DTO/ClaimsCountByStatesDTO';
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
  constructor(private claimService:ClaimService){

  }

  ChangeState(idState:number){
    this.claimService.GetAllClaimsByState(idState).subscribe((data)=>{
      console.log(data);
      
      this.ClaimsList = data;
    })
  }
  ngOnInit(): void {
    this.claimService.GetAllClaimsByState(0).subscribe((data)=>{
      this.ClaimsList = data;
    })
    this.claimService.GetCountClaimsByState().subscribe((data)=>{
      console.log(data);
      
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

}
