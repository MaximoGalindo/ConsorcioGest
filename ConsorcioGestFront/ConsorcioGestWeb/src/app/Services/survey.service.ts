import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../Helpers/Envriorment';
import { QuestionOptionDTO, ReplySurveyDTO, SurveyDTO, SurveyDetailDTO } from '../Models/DTO/ReplySurveyDTO';
import { FilterSurveyDTO } from '../Models/DTO/FiltersDTO';
import { ListItemDTO } from '../Models/HelperModel/ListItemDTO';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {  
  baseUrl =`${environment.API_URL}/survey/`;
  constructor(private http:HttpClient) { }

  CheckSurveyCompleted(id:number):Observable<any>{
    return this.http.get<any>(this.baseUrl + 'check-survey-completed/'+ id)
  }

  GetQuestionSurvey():Observable<QuestionOptionDTO[]>{
    return this.http.get<QuestionOptionDTO[]>(this.baseUrl + 'get-survey-questions-options')
  }

  SaveReplySurvey(replySurvey:ReplySurveyDTO):Observable<any>{
    return this.http.post<any>(this.baseUrl + 'save-reply-survey', replySurvey)
  }

  GetSurveys(filter:FilterSurveyDTO):Observable<SurveyDTO[]>{

    let params = new HttpParams();
    if (filter.stateID) {
      params = params.append('StateID', filter.stateID.toString());
    }
    if (filter.claimNumber) {
      params = params.append('ClaimNumber', filter.claimNumber.toString());
    }
    if (filter.dateFrom) {
      params = params.append('DateFrom', filter.dateFrom);
    }
    if (filter.dateTo) {
      params = params.append('DateTo', filter.dateTo);
    }   
    return this.http.get<SurveyDTO[]>(this.baseUrl + 'get-surveys',{params})
  }

  GetSurveyDetail(id:number):Observable<SurveyDetailDTO[]>{
    return this.http.get<SurveyDetailDTO[]>(this.baseUrl + 'get-survey-detail/'+ id)
  }

  GetSurveyState():Observable<ListItemDTO[]>{
    return this.http.get<ListItemDTO[]>(this.baseUrl + 'get-survey-states')
  }

}
