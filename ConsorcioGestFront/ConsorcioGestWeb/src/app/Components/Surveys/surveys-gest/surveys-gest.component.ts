import { Component, OnInit } from '@angular/core';
import { SurveyDTO } from 'src/app/Models/DTO/ReplySurveyDTO';
import { SurveyService } from 'src/app/Services/survey.service';

@Component({
  selector: 'app-surveys-gest',
  templateUrl: './surveys-gest.component.html',
  styleUrls: ['./surveys-gest.component.css']
})
export class SurveysGestComponent implements OnInit {

  _ShowModal:Boolean = false;

  Surveys: SurveyDTO[] = []
  SelectedSurvey:number = 0;
  constructor(private surveyService: SurveyService) {

  }
  ngOnInit(): void {
    this.surveyService.GetSurveys().subscribe((data) => {
      console.log(data);

      this.Surveys = data
    })
  }

  getSatisfactionClass(satisfaction: string | null | undefined): string {
    if (!satisfaction) {
      return 'satisfaction-no-data'; 
    }
    switch (satisfaction.toLowerCase()) {
      case 'green':
        return 'satisfaction-green';
      case 'yellow':
        return 'satisfaction-yellow';
      case 'red':
        return 'satisfaction-red';
      default:
        return 'satisfaction-no-data'; 
    }
  }

  getTextSatisfaction(satisfaction: string | null | undefined): string {
    if (!satisfaction) {
      return 'Encuesta no Respondida'; 
    }
    switch (satisfaction.toLowerCase()) {
      case 'green':
        return 'Muy Satisfecho';
      case 'yellow':
        return 'Regular';
      case 'red':
        return 'Insatisfecho';
      default:
        return 'Encuesta no Respondida'; 
    }
  }

  ShowSurvey(ID:number){
    this.SelectedSurvey = ID;
    this._ShowModal = true;
  }

  CloseModal(){
    this._ShowModal = false;
  }
} 
