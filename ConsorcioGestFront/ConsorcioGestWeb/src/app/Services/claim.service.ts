import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConsortiumConfiguration, Tower } from '../Models/Models/ConsortiumConfigModel';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';
import { environment } from '../Helpers/Envriorment';
import { ClaimUserDTO } from '../Models/DTO/ClaimUserDTO';
import { ClaimDTO } from '../Models/DTO/ClaimDTO';
import { ClaimsCountByStatesDTO } from '../Models/DTO/ClaimsCountByStatesDTO';
import { HistoryClaimDTO } from '../Models/DTO/HistoryClaimDTO';
import { ClaimGestDTO } from '../Models/DTO/ClaimGestDTO';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  baseUrl =`${environment.API_URL}/claims/`;
  constructor(private http:HttpClient) { }

  GetImages(idReclamo: number): Observable<any> {
    return this.http.get(`${this.baseUrl}${idReclamo}`);
  }

  GetCauseClaim(){
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'cause-claims-list')
  }

  GetAffectedSpace(){
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'affected-space-list')
  }

  GetStatesClaim(){
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'states-claims-list')
  }

  GetHistoryClaim(claimID:number):Observable<HistoryClaimDTO[]> {
    return this.http.get<HistoryClaimDTO[]>(this.baseUrl + `histoty-claim-list/${claimID}`)
  }

  GetAllClaimsByState(idState:number):Observable<ClaimDTO[]>{
    return this.http.get<ClaimDTO[]>(this.baseUrl + 'get-claims-by-state/'+ idState)
  }

  GetClaimsByUserID(userID:number):Observable<ClaimDTO[]>{
    return this.http.get<ClaimDTO[]>(this.baseUrl + 'get-claims-by-user/'+ userID)
  }

  GetCountClaimsByState():Observable<ClaimsCountByStatesDTO[]>{
    return this.http.get<ClaimsCountByStatesDTO[]>(this.baseUrl + 'get-claims-count-by-state')
  }


  SaveClaim(Claim:ClaimUserDTO):Observable<any>{
    const formData = new FormData();
    formData.append('CauseClaimID', Claim.CauseClaim.toString());
    formData.append('AffectedSpaceID', Claim.AffectedSpace.toString());
    formData.append('ProblemDetail', Claim.ProblemDetail);
  
    for (let i = 0; i < Claim.Images.length; i++) {
      formData.append('Files', Claim.Images[i]);
    }
    return this.http.post<any>(this.baseUrl + 'save-claim-user', formData)
  }

  SaveClaimGestion(SaveClaimGest:ClaimGestDTO):Observable<any>{
    return this.http.post<any>(this.baseUrl + 'save-claim-gestion', SaveClaimGest)  
  }

}
