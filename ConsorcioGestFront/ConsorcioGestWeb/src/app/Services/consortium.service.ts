import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConsortiumConfiguration, Tower } from '../Models/Models/ConsortiumConfigModel';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';
import { environment } from '../Helpers/Envriorment';

@Injectable({
  providedIn: 'root'
})
export class ConsortiumService {

  baseUrl = `${environment.API_URL}/consortium/`;
  constructor(private http:HttpClient) { }

  GetConsortiums():Observable<any>{
    return this.http.get(this.baseUrl + 'get-consortiums')
  }

  GenerateLogicConfiguration(config:Tower):Observable<any>{
    return this.http.post(this.baseUrl + 'generate-logic-configuration',config)
  }

  SaveConsortium(consortiumConfig:ConsortiumConfiguration):Observable<any>{
    return this.http.post(this.baseUrl + 'save-consortium',consortiumConfig)
  }

  GetCondominiums(tower:string):Observable<ListItemDTO[]>{
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'get-condominiums?Tower=' + tower)
  }
  GetTowers():Observable<ListItemDTO[]>{
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'get-towers')
  }

  GetCommonSpaces():Observable<ListItemDTO[]>{
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'get-common-spaces')
  }

}
