using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class QuestionDTO
    {
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public string Comment { get; set; }
    }

    public class ReplySurveyDTO 
    {
        public int IdSurvey { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }

    public class QuestionOptionDTO
    {
        public int QuestionID { get; set; }
        public string Question {  get; set; }
        public string AnswerType { get; set; }
        public List<OptionDTO> Options { get; set; }
    }

    public class OptionDTO 
    {
        public int OptionID { get; set; }   
        public string Option { get; set; }
        public int? ValueNumeric { get; set; }

    }

    public class SurveyDTO 
    {
        public int Id {  get; set; }
        public UserModelDTO User { get; set; }
        public int SurveyStateID { get; set; }
        public string SurveyState {  get; set; }
        public string ClaimNumber { get; set; }
        public string CustomerSatisfaccion { get; set; }
    }

    public class  SurveyDetailDTO
    {
        public string Question { get; set;}
        public string Awnser { get; set; }
        public string Comment { get; set; }
    }

}
