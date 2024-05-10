import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConsortiumConfiguration, Tower } from '../Models/Models/ConsortiumConfigModel';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  baseUrl = 'https://localhost:7083/claim/';
  constructor(private http:HttpClient) { }

  SaveConsortium(imageByte:Uint8Array):Observable<any>{
    return this.http.post(this.baseUrl + 'guardar-imagen',imageByte)
  }


}
