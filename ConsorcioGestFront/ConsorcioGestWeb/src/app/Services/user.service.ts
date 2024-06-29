import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterUserDTO } from '../Models/DTO/RegisterUserDTO';
import { UpdateUserDTO } from '../Models/DTO/UpdateUserDTO';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';
import { UserModelByDocumentDTO, UserModelDTO } from '../Models/DTO/UserModelDTO';
import { environment } from '../Helpers/Envriorment';
import { FilterUserDTO } from '../Models/DTO/FiltersDTO';
import { ClaimDTO } from '../Models/DTO/ClaimDTO';

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

  GetUsers(filter:FilterUserDTO):Observable<UserModelDTO[]>{
    let params = new HttpParams();
    if (filter.document) {
      params = params.append('Document', filter.document.toString());
    }
    if (filter.userStateID) {
      params = params.append('UserStateID', filter.userStateID.toString());
    }
    if (filter.tower) {
      params = params.append('Tower', filter.tower);
    }
    if (filter.codominium) {
      params = params.append('Condominium', filter.codominium);
    }
    return this.http.get<UserModelDTO[]>(`${this.baseUrl}get-users`, { params });
  }

  GetUserByDocument(userDocument:number): Observable<UserModelByDocumentDTO>{
    return this.http.get<UserModelByDocumentDTO>(this.baseUrl + 'get-user-by-document/'+ userDocument)
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
