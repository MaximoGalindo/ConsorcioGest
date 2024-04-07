import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'https://localhost:7083/';
  constructor(private http:HttpClient) { }

  login(credentials:any) :Observable<string>{
    return this.http.post(this.baseUrl + 'api/login', credentials, { responseType: 'text' });
  }
  
  getUser(): Observable<any> {
    return this.http.get(this.baseUrl + 'getUser')
  }
  //ACA SE HACE TAMBIEN EL REGISTER
}
