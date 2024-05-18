import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ClaimDTO } from 'src/app/Models/DTO/ClaimDTO';
import { ClaimGestDTO } from 'src/app/Models/DTO/ClaimGestDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { ClaimService } from 'src/app/Services/claim.service';

@Component({
  selector: 'app-see-more-claim',
  templateUrl: './see-more-claim.component.html',
  styleUrls: ['./see-more-claim.component.css']
})
export class SeeMoreClaimComponent implements OnInit{

  @Output() _ShowModal = new EventEmitter<boolean>();

  @Input() Claim:ClaimDTO = new ClaimDTO();

  ShowCarrusel:boolean = false;

  saveClaimGest:ClaimGestDTO = new ClaimGestDTO();
  statesClaim:ListItemDTO[] = [];
  images:any[] = [];
  constructor(private claimService:ClaimService){

  }

  ngOnInit(){
    this.claimService.GetStatesClaim().subscribe((data)=>{
      this.statesClaim = data;
    })
    this.claimService.GetImages(this.Claim.id).subscribe((data)=>{
      this.images = data;
      console.log(this.images);
      
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
      this._ShowModal.emit(false);
    })   
  }
}
