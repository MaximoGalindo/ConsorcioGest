import { UserModelDTO } from "./UserModelDTO";

export class QuestionDTO {
  QuestionID: number = 0;
  AnswerID: number = 0;
  Comment: string = '';
}

export class ReplySurveyDTO {
  IdSurvey: number = 0;
  Questions: QuestionDTO[] = [];
}

export class QuestionOptionDTO {
  questionID: number = 0;
  question: string = '';
  answerType: string = '';
  options: OptionDTO[] = [];
  optionSelectedID: number = 0;
  comment: string = '';
}

export class OptionDTO {
  optionID: number = 0;
  option: string = '';
  valueNumeric: number = 0;
}

export class SurveyDTO {
  id: number = 0;
  user: UserModelDTO = new UserModelDTO();
  surveyStateID: number = 0;
  surveyState: string = '';
  claimNumber: string = '';
  customerSatisfaccion: string = '';
}

export class SurveyDetailDTO {
  question: string = '';
  awnser: string = '';
  comment: string = '';
}


