import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tower } from '../Models/Models/ConsortiumConfigModel';

@Injectable({
  providedIn: 'root'
})
export class ConsortiumService {

  baseUrl = 'https://localhost:7083/consortium/';
  constructor(private http:HttpClient) { }

  getCurrentConsortium(consortiumID:number) :Observable<any>{
    return this.http.post(this.baseUrl + 'getConsortium?consortiumID='+ consortiumID,{});
  }  
  GetConsortiums():Observable<any>{
    return this.http.get(this.baseUrl + 'getConsortiums')
  }

  GenerateLogicConfiguration(config:Tower):Observable<any>{
    return this.http.post(this.baseUrl + 'generateLogicConfiguration',config)
  }
}
