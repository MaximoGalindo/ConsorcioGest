import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
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
  constructor(
    private claimService:ClaimService,
    private http: HttpClient
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

  //ARREGLAR ESTO PORQUE SE DEBE SUBIR TODO EN EL DTO DE RECLAMOS
  seleccionarImagen(event: any) {
    const archivosSeleccionados = event.target.files;
    for (let i = 0; i < archivosSeleccionados.length; i++) {
      this.imagenesSeleccionadas.push(archivosSeleccionados[i]);
    }
    console.log(this.imagenesSeleccionadas);
    
    /*const archivoSeleccionado = event.target.files[0];
    if (archivoSeleccionado) {
      this.imagenesSeleccionadas = archivoSeleccionado;  
      this.claimService.SaveImage(archivoSeleccionado)
        .subscribe(
          (response) => {
            console.log('Imagen subida con éxito:', response);            
          },
          (error) => {
            console.error('Error al subir la imagen:', error);
          }
        ); 
    }*/
  }
   
  /*obtenerImagen(id: number) {
    this.claimService.GetImages(id)
        .subscribe(
            (imagen: Blob) => {
                const reader = new FileReader();
                reader.onload = () => {
                    this.imagenUrl = reader.result as string;
                };
                reader.readAsDataURL(imagen);
            },
            error => {
                console.error('Error al obtener la imagen:', error);
            }
        );
      }*/
}
