import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConsortiumConfiguration, Tower } from '../Models/Models/ConsortiumConfigModel';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';
import { environment } from '../Helpers/Envriorment';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  baseUrl =`${environment.API_URL}/claims/`;
  constructor(private http:HttpClient) { }

  SaveImage(imagen:File):Observable<any>{
    const formData = new FormData();
    formData.append('archivo', imagen);
    return this.http.post(this.baseUrl + 'guardar-imagen', formData);
  }

  GetImages(idReclamo: number): Observable<any> {
    return this.http.get(`${this.baseUrl}${idReclamo}`, { responseType: 'blob' });
  }

  GetCauseClaim(){
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'cause-claims-list')
  }

  GetAffectedSpace(){
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'affected-space-list')
  }

}
