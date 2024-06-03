import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { QuestionDTO, QuestionOptionDTO, ReplySurveyDTO } from 'src/app/Models/DTO/ReplySurveyDTO';
import { ClaimService } from 'src/app/Services/claim.service';

@Component({
  selector: 'app-newsurvey',
  templateUrl: './newsurvey.component.html',
  styleUrls: ['./newsurvey.component.css']
})
export class NewsurveyComponent implements OnInit {

  _IsCompleted: Boolean = false;
  selectedTab: number = 1;
  questionOptions: QuestionOptionDTO[] = [];
  surveyID: number = 0;
  constructor( private claimService:ClaimService,private route: ActivatedRoute,) {
    this.surveyID = this.route.snapshot.params['surveyID'];
    
  }

  ngOnInit(){    
    this.claimService.GetQuestionSurvey().subscribe((data)=>{
      this.questionOptions = data 
      console.log(this.questionOptions);      
    })
    this.claimService.CheckSurveyCompleted(this.surveyID).subscribe((data)=>{
      this._IsCompleted = data;
    })
  }

  Back(){
    this.selectedTab = this.selectedTab > 1   ? this.selectedTab - 1 : this.selectedTab
  }
  Next(){
    this.selectedTab = this.selectedTab < 5 ? this.selectedTab + 1 : this.selectedTab
  }
  Confirm() {
    console.log(this.questionOptions);

    var replySurvey = new ReplySurveyDTO();
    replySurvey.IdSurvey = this.surveyID;  

    var anwsers: QuestionDTO[] = [];
    for(var q of this.questionOptions){  
      anwsers.push({QuestionID: q.questionID, AnswerID: Number(q.optionSelectedID), Comment: q.comment !== undefined ? q.comment : ''});
    }
    replySurvey.Questions = anwsers;
    console.log(replySurvey);
    this.claimService.SaveReplySurvey(replySurvey).subscribe((data)=>{
      console.log(data);      
    })

  }

  ShowTextArea(optionID: number, index: number): boolean {
    var option = this.questionOptions[index].options.find(option => option.optionID == optionID);
    if (option && 
        option.valueNumeric !== undefined &&              
        (option.option == 'NO' || (option.valueNumeric <= 2 && option.valueNumeric != null))) {
      return true;
    }
    return false;
  }
  
}
