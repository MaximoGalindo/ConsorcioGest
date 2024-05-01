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

  baseUrl = 'https://localhost:7083/users/';
  constructor(private http:HttpClient) { }

  CreateUser(registerUserDTO: RegisterUserDTO): Observable<any> {
    return this.http.post(this.baseUrl + 'register', registerUserDTO)
  }

  GetDocumentTypes(): Observable<any> {
    return this.http.get(this.baseUrl + 'get-document-types')
  }

  GetUsers():Observable<UserModelDTO[]>{
    return this.http.get<UserModelDTO[]>(this.baseUrl + 'get-users')
  }

  GetUserByDocument(userDocument:number): Observable<UserModelDTO>{
    return this.http.get<UserModelDTO>(this.baseUrl + 'get-user-by-document/'+ userDocument)
  }

  UpdateUser(userDocument:number, user:UpdateUserDTO):Observable<any>{
    return this.http.put(this.baseUrl + 'update-user/'+ userDocument, user)
  }

  
  
}
