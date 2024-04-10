import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../Models/Models/UserModel';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'https://localhost:7083/';
  constructor(private http:HttpClient) { }

  login(credentials:any) :Observable<UserModel>{
    return this.http.post<UserModel>(this.baseUrl + 'api/login', credentials);
  }
  
  getUser(): Observable<any> {
    return this.http.get(this.baseUrl + 'getUser')
  }
  //ACA SE HACE TAMBIEN EL REGISTER
}
