import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConsortiumConfiguration, Tower } from '../Models/Models/ConsortiumConfigModel';

@Injectable({
  providedIn: 'root'
})
export class ConsortiumService {

  baseUrl = 'https://localhost:7083/consortium/';
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
}
