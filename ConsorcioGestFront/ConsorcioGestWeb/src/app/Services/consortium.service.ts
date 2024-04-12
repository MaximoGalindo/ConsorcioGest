import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConsortiumService {

  baseUrl = 'https://localhost:7083/';
  constructor(private http:HttpClient) { }

  getCurrentConsortium(consortiumID:number) :Observable<any>{
    return this.http.post(this.baseUrl + 'getConsortium?consortiumID='+ consortiumID,{});
  }
}
