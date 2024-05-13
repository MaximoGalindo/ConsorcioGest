import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClaimUserDTO } from 'src/app/Models/DTO/ClaimUserDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { ClaimService } from 'src/app/Services/claim.service';

@Component({
  selector: 'app-user-register-claim',
  templateUrl: './user-register-claim.component.html',
  styleUrls: ['./user-register-claim.component.css']
})
export class UserRegisterClaimComponent implements OnInit {
  imagenesSeleccionadas: File[] = [];
  claim:ClaimUserDTO = new ClaimUserDTO();
  causeClaim:ListItemDTO[] = [];
  affectedSpaces:ListItemDTO[] = [];
  success:boolean = false;
  claimNumber:string = "";
  constructor(
    private claimService:ClaimService,
    private router:Router
  ){

  }
  ngOnInit(): void {
    this.claimService.GetCauseClaim().subscribe((data)=>{
      this.causeClaim = data;
    });
    this.claimService.GetAffectedSpace().subscribe((data)=>{
      this.affectedSpaces = data;
    })
  }
  seleccionarImagen(event: any) {
    const archivosSeleccionados = event.target.files;
    for (let i = 0; i < archivosSeleccionados.length; i++) {
      this.imagenesSeleccionadas.push(archivosSeleccionados[i]);
    }
    this.claim.Images = this.imagenesSeleccionadas;
  }

  Save(){
    this.claimService.SaveClaim(this.claim).subscribe((data)=>{
      this.claimNumber = data.result;
      this.success = true;  
    })
  }
  Continue(){
    this.success = false;
    this.router.navigate(['/main-page-user'])
  }

}
