import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Utils } from 'src/app/Helpers/Utils';
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
  historyClaim:any[] = [];

  loading:boolean = false;

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
    this.loading = true;
    this.claimService.SaveClaimGestion(this.saveClaimGest).subscribe({
      next: () => {
        this._ShowModal.emit(true);
        this._Reaload.emit(true);
        this.loading = false;
        Utils.success("Guardado con exito");
      },
      error: () => {
        this.loading = false;
        Utils.error("Error al guardar");
      }
    })   
  }

  showHistory = false;

  toggleHistory() {
    this.showHistory = !this.showHistory;
    if(this.showHistory)
      this.LoadHistorial()
  }

  LoadHistorial(){  
    console.log(this.Claim.id);
      
    this.claimService.GetHistoryClaim(this.Claim.id).subscribe((data)=>{
      this.historyClaim = data;
      this.historyClaim.forEach(element => {
        switch (element.state) {
          case "IN_PROGRESS":
            element.state = "En Progreso";
            break;
          case "CANCELLED":
            element.state = "Cancelado";
            break;
          case "FINISHED":
            element.state = "Finalizado";
            break;
          case "PENDING":
            element.state = "Pendiente";
            break;
          default:
            break;
        }
      })
      
    })
  }
}
