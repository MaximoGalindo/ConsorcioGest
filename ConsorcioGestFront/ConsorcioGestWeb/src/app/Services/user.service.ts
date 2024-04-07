import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterUserDTO } from '../Models/DTO/RegisterUserDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:7083/';
  constructor(private http:HttpClient) { }

  createUser(registerUserDTO: RegisterUserDTO): Observable<any> {
    return this.http.post(this.baseUrl + 'register', registerUserDTO)

  }

  getDocumentTypes(): Observable<any> {
    return this.http.get(this.baseUrl + 'getDocumentTypes')
  }

  //ACA VAS A PREGUNTAR TODO LO DEL USUARIO
}
