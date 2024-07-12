import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';
import { ClaimGestDTO } from 'src/app/Models/DTO/ClaimGestDTO';
import { UserModelByDocumentDTO } from 'src/app/Models/DTO/UserModelDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { ClaimService } from 'src/app/Services/claim.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-see-more-claim',
  templateUrl: './see-more-claim.component.html',
  styleUrls: ['./see-more-claim.component.css']
})
export class SeeMoreClaimComponent implements OnInit{

  @Output() _ShowModal = new EventEmitter<boolean>();
  @Output() _Reaload = new EventEmitter<boolean>();

  @Input() Claim:ClaimDTO = new ClaimDTO();
  user:UserModelByDocumentDTO = new UserModelByDocumentDTO();
  ShowCarrusel:boolean = false;

  saveClaimGest:ClaimGestDTO = new ClaimGestDTO();
  statesClaim:ListItemDTO[] = [];
  images:any[] = [];
  constructor(private claimService:ClaimService,
    private userService:UserService
  ){

  }

  ngOnInit(){
    this.userService.GetUserByID(this.Claim.userID).subscribe((data)=>{
      this.user = data;
    })
    
    this.claimService.GetStatesClaim().subscribe((data) => {  
      if(this.Claim.stateId === 1)
        this.statesClaim = data.filter(x => x.id !== 1);
      else if (this.Claim.stateId === 2)
        this.statesClaim = data.filter(x => x.id !== 2);
      else if (this.Claim.stateId === 3)
        this.statesClaim = data.filter(x => x.id !== 3);
      else
        this.statesClaim = data.filter(x => x.id !== 4);
      
    });
    this.claimService.GetImages(this.Claim.id).subscribe((data)=>{
      this.images = data;      
    })
  }

  SeeImages(){
    this.ShowCarrusel = true;
  }

  CloseModal(){
    this._ShowModal.emit(false);
  }

  CloseCarrusel(){
    this.ShowCarrusel = false;
  }

  SaveClaimGestion(){
    this.saveClaimGest.IdClaim = this.Claim.id
    this.claimService.SaveClaimGestion(this.saveClaimGest).subscribe((data)=>{
      this._ShowModal.emit(true);
      this._Reaload.emit(true);
    })   
  }
}
