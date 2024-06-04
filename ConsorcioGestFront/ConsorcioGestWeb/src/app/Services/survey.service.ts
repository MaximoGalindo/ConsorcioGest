import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../Helpers/Envriorment';
import { QuestionOptionDTO, ReplySurveyDTO } from '../Models/DTO/ReplySurveyDTO';

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
}
