import { Component } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';
import { FilterClaimUserDTO } from 'src/app/Models/DTO/FiltersDTO';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { UserDataSharedService } from 'src/app/Services/Shared/user-data-shared.service';
import { ClaimService } from 'src/app/Services/claim.service';

@Component({
  selector: 'app-claim-tracking',
  templateUrl: './claim-tracking.component.html',
  styleUrls: ['./claim-tracking.component.css']
})
export class ClaimTrackingComponent {
  
  claimsDTO:ClaimDTO[] = []
  User:UserModelDTO = new UserModelDTO();
  causeClaims:ListItemDTO[] = [];

  //FILTERS
  selectedCauseClaim:ListItemDTO = new ListItemDTO();
  dateFrom:string = "";
  dateTo:string = "";
  claimNumber:string = "";

  constructor(private claimService:ClaimService){

  }

  ngOnInit(){
    this.claimService.GetCauseClaim().subscribe((data)=>{
      this.causeClaims = data;
    })
    var filter = new FilterClaimUserDTO();
    this.claimService.GetClaimsByUser(filter).subscribe((data)=>{
      this.claimsDTO = data;
    })
  }

  Search(){   
    var filter = new FilterClaimUserDTO ();
    filter.causeClaim = this.selectedCauseClaim.id;
    filter.nroReclamo = this.claimNumber != '' ? this.claimNumber : '';
    filter.dateFrom = this.dateFrom != '' ? this.dateFrom : '';
    filter.dateTo = this.dateTo != '' ? this.dateTo : '';
    this.claimService.GetClaimsByUser(filter).subscribe((data)=>{
      this.claimsDTO = data;
    })
  }

}
