import { Component } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
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
  constructor(private claimService:ClaimService,
    private SharedDataUser:UserDataSharedService
  ){

  }

  ngOnInit(){
    this.SharedDataUser.User$.subscribe((data)=>{
      if(data != null)
        this.User = data;
    }) 
    this.claimService.GetClaimsByUserID(this.User.id).subscribe((data)=>{
      this.claimsDTO = data;
      console.log(this.claimsDTO);
      
    })
  }

}
