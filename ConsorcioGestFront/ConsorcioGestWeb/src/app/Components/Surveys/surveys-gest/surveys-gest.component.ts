import { Component, OnInit } from '@angular/core';
import { filter } from 'rxjs';
import { FilterSurveyDTO } from 'src/app/Models/DTO/FiltersDTO';
import { SurveyDTO } from 'src/app/Models/DTO/ReplySurveyDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
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
  surveyStates:ListItemDTO[] = []

  selectedState:number = 0;
  dateFrom:string = "";
  dateTo:string = "";
  claimNumber:string = "";

  constructor(private surveyService: SurveyService) {

  }
  ngOnInit(): void {

    var filter = new FilterSurveyDTO();

    this.surveyService.GetSurveys(filter).subscribe((data) => {
      console.log(data);

      this.Surveys = data
    })

    this.surveyService.GetSurveyState().subscribe((data)=>{
      this.surveyStates = data
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
      case 'Unaswerred':
        return 'Encuesta no Respondida'; 
    }
    return '';
  }

  ShowSurvey(ID:number){
    this.SelectedSurvey = ID;
    this._ShowModal = true;
  }

  CloseModal(){
    this._ShowModal = false;
  }

  Search(){
    var filter = new FilterSurveyDTO ();
    
    filter.stateID = this.selectedState != null ? this.selectedState : 0
    filter.claimNumber = this.claimNumber != '' ? this.claimNumber : '';
    filter.dateFrom = this.dateFrom != '' ? this.dateFrom : '';
    filter.dateTo = this.dateTo != '' ? this.dateTo : '';

    this.surveyService.GetSurveys(filter).subscribe((data)=>{
      this.Surveys = data
    })
  }
} 
