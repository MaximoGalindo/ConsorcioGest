import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SurveyDTO, SurveyDetailDTO } from 'src/app/Models/DTO/ReplySurveyDTO';
import { SurveyService } from 'src/app/Services/survey.service';

@Component({
  selector: 'app-see-more-survey',
  templateUrl: './see-more-survey.component.html',
  styleUrls: ['./see-more-survey.component.css']
})
export class SeeMoreSurveyComponent implements OnInit{

  @Output() _CloseModal =  new EventEmitter<Boolean>();

  @Input() SurveyID: number = 0;
  SurveyDetail:SurveyDetailDTO[] = [];

  constructor(private surveyService:SurveyService){

  }

  ngOnInit(){
    this.surveyService.GetSurveyDetail(this.SurveyID).subscribe((data)=>{
      this.SurveyDetail = data
    })
  }

  CloseModal(){
    this._CloseModal.emit(false);
  }
}
