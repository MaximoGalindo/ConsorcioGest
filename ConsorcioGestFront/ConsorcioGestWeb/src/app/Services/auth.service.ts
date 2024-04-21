import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../Models/Models/UserModel';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'https://localhost:7083/login/';
  constructor(private http:HttpClient) { }

  login(credentials:any) :Observable<UserModel>{
    return this.http.post<UserModel>(this.baseUrl, credentials);
  }

  SetCurrentConsortium(consortiumID:number) :Observable<any>{
    return this.http.post<any>(this.baseUrl + "setCurrentConsortium?consortiumID=" + consortiumID, null);
  }
  
}
