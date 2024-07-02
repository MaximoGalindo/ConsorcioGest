import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../Models/Models/UserModel';
import { environment } from '../Helpers/Envriorment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = `${environment.API_URL}/login/`;
  constructor(private http:HttpClient) { }

  login(credentials:any) :Observable<UserModel>{
    return this.http.post<UserModel>(this.baseUrl, credentials);
  }

  SetCurrentConsortium(consortiumID:number) :Observable<any>{
    return this.http.post<any>(this.baseUrl + "set-current-consortium?consortiumID=" + consortiumID, null);
  }
  
  RemoveCurrentConsortium() :Observable<any>{
    return this.http.post<any>(this.baseUrl + "remove-current-consortium", null);
  }

  Logout():Observable<any>{
    return this.http.post<any>(this.baseUrl + "logout", null);
  }
}
