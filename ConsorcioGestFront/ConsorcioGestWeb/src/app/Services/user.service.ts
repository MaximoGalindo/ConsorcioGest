import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterUserDTO } from '../Models/DTO/RegisterUserDTO';
import { UserModelDTO } from '../Models/DTO/UserModelDTO';
import { UpdateUserDTO } from '../Models/DTO/UpdateUserDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:7083/';
  constructor(private http:HttpClient) { }

  CreateUser(registerUserDTO: RegisterUserDTO): Observable<any> {
    return this.http.post(this.baseUrl + 'register', registerUserDTO)
  }

  GetDocumentTypes(): Observable<any> {
    return this.http.get(this.baseUrl + 'getDocumentTypes')
  }

  GetConsortiums():Observable<any>{
    return this.http.get(this.baseUrl + 'getConsortiums')
  }

  GetUsers():Observable<UserModelDTO[]>{
    return this.http.get<UserModelDTO[]>(this.baseUrl + 'getUsers')
  }

  GetUserByDocument(userDocument:number): Observable<UserModelDTO>{
    return this.http.get<UserModelDTO>(this.baseUrl + 'getUserByDocument/'+ userDocument)
  }

  UpdateUser(userDocument:number, user:UpdateUserDTO):Observable<any>{
    return this.http.put(this.baseUrl + 'updateUser/'+ userDocument, user)
  }

  
  
}
