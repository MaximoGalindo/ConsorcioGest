import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterUserDTO } from '../Models/DTO/RegisterUserDTO';
import { UpdateUserDTO } from '../Models/DTO/UpdateUserDTO';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';
import { UserModelDTO } from '../Models/DTO/UserModelDTO';
import { environment } from '../Helpers/Envriorment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = `${environment.API_URL}/users/`;
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

  GetProfiles():Observable<ListItemDTO[]>{
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'get-profiles')
  }
  
  GetStatus():Observable<ListItemDTO[]>{
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'get-states')
  }
  
}
