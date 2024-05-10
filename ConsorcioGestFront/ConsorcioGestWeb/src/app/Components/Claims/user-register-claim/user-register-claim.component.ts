import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ClaimService } from 'src/app/Services/claim.service';

@Component({
  selector: 'app-user-register-claim',
  templateUrl: './user-register-claim.component.html',
  styleUrls: ['./user-register-claim.component.css']
})
export class UserRegisterClaimComponent {

  constructor(
    private claimService:ClaimService,
    private http: HttpClient
  ){

  }
  imagenSeleccionada: File | null = null;
  imagenUrl: string = "";
  
  seleccionarImagen(event: any) {
    this.imagenSeleccionada = event.target.files[0] as File;
  }
  
  subirImagen() {
    // Verificar si se ha seleccionado una imagen
    if (!this.imagenSeleccionada) {
      console.error('No se ha seleccionado ninguna imagen');
      return;
    }
  
    const reader = new FileReader();
    reader.onload = (event: ProgressEvent<FileReader>) => {
      // Obtener los bytes de la imagen como un ArrayBuffer
      const bytes = (event.target?.result) as ArrayBuffer;
      // Convertir los bytes a un Uint8Array
      const byteArray = new Uint8Array(bytes);
  
      // Enviar los bytes en la solicitud POST
      this.http.post('https://localhost:7083/claims/guardar-imagen', byteArray).subscribe(() => {
        console.log('Imagen subida correctamente');
      });
    };
  
    // Leer la imagen como ArrayBuffer
    reader.readAsArrayBuffer(this.imagenSeleccionada);
  }
  
  
  
  obtenerImagen(id: number) {
    this.http.get(`https://localhost:7083/claim/obtener-imagen`, { responseType: 'blob' })
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
      }
}
