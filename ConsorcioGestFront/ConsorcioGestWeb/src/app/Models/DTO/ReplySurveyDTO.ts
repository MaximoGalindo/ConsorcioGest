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
