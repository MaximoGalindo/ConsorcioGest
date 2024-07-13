using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Resourses;
using BusinessService.Services.BaseService;
using BusinessService.Templates;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class SurveyService: BaseService<Encuesta>
    {
        public readonly ConsorcioGestContext context;
        public readonly EmailService emailService;

        public SurveyService(ConsorcioGestContext context,EmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
        }
            
        public async Task<bool> CreateSurvey(int claimID)
        {
            var consortiumID = context.Reclamos.Where(r => r.Id == claimID).Select(r => r.IdUsuarioNavigation.ConsorcioUsuarios.Select(c => c.IdConsorcio).FirstOrDefault()).FirstOrDefault();
            var encuestaExist = context.Encuestas.Any(e => e.IdReclamo == claimID);

            if(!encuestaExist)
            {
                Encuesta encuesta = new Encuesta
                {
                    IdReclamo = claimID,
                    IdEstadoEncuesta = (int)SurveyStatesEnum.INITIATED,
                    Fecha = DateTime.Now.Date,
                    IdConsorcio = consortiumID
                };
                DBAdd(encuesta, context);
                await SendSurveyByEmail(claimID);
            }

            
            return true;
        }

        public bool SaveReplySurvey(ReplySurveyDTO replySurvey)
        {           
            foreach(var question in replySurvey.Questions)
            {
                EncuestasDetalle encuestasDetalle = new EncuestasDetalle
                {
                    IdEncuesta = replySurvey.IdSurvey,
                    IdPregunta = question.QuestionID,
                    IdOpcion = question.AnswerID,
                    Comentario = question.Comment
                };
                DBAdd(encuestasDetalle, context);
            };
            Encuesta encuesta = context.Encuestas.Where(e => e.Id == replySurvey.IdSurvey).FirstOrDefault();
            encuesta.IdEstadoEncuesta = (int)SurveyStatesEnum.COMPLETED;
            encuesta.Fecha = DateTime.Now.Date;
            DBUpdate(encuesta, context);
            return true;
        }

        public async Task<bool> SendSurveyByEmail(int claimID)
        {
            var surveyID = context.Encuestas.Where(e => e.IdReclamo == claimID).Select(e => e.Id).FirstOrDefault();
            var claim = context.Reclamos.Where(r => r.Id == claimID).Select(r => new 
            { 
                ClaimNumber = r.NroReclamo, 
                Email = r.IdUsuarioNavigation.Email 
            }).FirstOrDefault();

            var surveyLink = $"{GlobalResourses.FrontURL}/claim-survey/{surveyID}";

            var bodyEmail = EmailTemplateService.GetTemplate("EmailReplySurvey", ("claimNumber", claim.ClaimNumber), ("surveyLink",surveyLink));
;
            await emailService.SendEmailAsync(claim.Email, "Reclamo Resuelto",bodyEmail);
            return true;
        }

        public async Task<bool> CheckIfSurveyCompleted(int surveyID)
        {
            var checkSurveyCompleted = context.Encuestas.Where(e => e.Id == surveyID).Select(e => e.IdEstadoEncuesta).FirstOrDefault();
            return checkSurveyCompleted == (int)SurveyStatesEnum.COMPLETED ? true : false;
        }

        public List<QuestionOptionDTO> GetQuestionSurvey()
        {
            var query = context.PreguntaOpciones
                        .Include(po => po.IdPreguntaNavigation)
                        .Include(po => po.IdOpcionNavigation)
                        .GroupBy(po => new { po.IdPreguntaNavigation.Id, po.IdPreguntaNavigation.Pregunta })
                        .Select(group => new QuestionOptionDTO
                        {
                            QuestionID = group.Key.Id,
                            Question = group.Key.Pregunta,
                            AnswerType = group.Key.Id == 1 ? AnswerType.Radio.ToString() : AnswerType.Select.ToString(),
                            Options = group.Select(po => new OptionDTO
                            {
                                OptionID = po.IdOpcionNavigation.Id,
                                Option = po.IdOpcionNavigation.Opcion1,
                                ValueNumeric = po.IdOpcionNavigation.ValorNumerico != null ? po.IdOpcionNavigation.ValorNumerico.Value : null
                            }).ToList()
                        }).ToList();

            return query;
        }

        public List<SurveyDTO> GetSurveys(FilterSurveyDTO filterSurvey)
        {
            List<SurveyDTO> surveys = context.Encuestas
                .Where(e => e.IdConsorcio == LoginService.CurrentConsortium.Id
                            && (filterSurvey.StateID != 0 ? e.IdEstadoEncuesta == filterSurvey.StateID : true)
                            && (!string.IsNullOrEmpty(filterSurvey.ClaimNumber) ? e.IdReclamoNavigation.NroReclamo == filterSurvey.ClaimNumber: true)
                            && (filterSurvey.DateFrom != null ? e.Fecha >= filterSurvey.DateFrom : true)
                            && (filterSurvey.DateTo != null ? e.Fecha <= filterSurvey.DateTo : true))
                .Select(e => new SurveyDTO
                {
                    Id = e.Id,
                    User = new UserModelDTO
                    {
                        Document = e.IdReclamoNavigation.IdUsuarioNavigation.Documento,
                        Name = e.IdReclamoNavigation.IdUsuarioNavigation.Nombre + ' ' + e.IdReclamoNavigation.IdUsuarioNavigation.Apellido,
                        Condominium = e.IdReclamoNavigation.IdUsuarioNavigation.IdCondominioNavigation.Torre + ' ' + e.IdReclamoNavigation.IdUsuarioNavigation.IdCondominioNavigation.NumeroDepartamento,
                    },
                    SurveyStateID = e.IdEstadoEncuesta,
                    SurveyState = e.IdEstadoEncuestaNavigation.Nombre,
                    ClaimNumber = e.IdReclamoNavigation.NroReclamo
                })
                .ToList();

            foreach(var s in surveys)
            {
                if(s.SurveyStateID == (int)SurveyStatesEnum.COMPLETED)
                    s.CustomerSatisfaccion = GetCustomerSatisfaccion(s.Id).ToString();
            }

            return surveys;
        }

        public List<SurveyDetailDTO> GetSurveyDetailById(int surveyID)
        {
            return context.EncuestasDetalles
                .Where(ed => ed.IdEncuesta == surveyID)
                .Select(ed => new SurveyDetailDTO
                {
                    Question = ed.IdPreguntaNavigation.Pregunta,
                    Awnser = ed.IdOpcionNavigation.Opcion1,
                    Comment = ed.Comentario,
                })
                .ToList();
        }


        public CustomerSatisfaccion GetCustomerSatisfaccion(int surveyID)
        {
            var customerSatisfaccion = context.EncuestasDetalles
                .Where(ed => ed.IdEncuesta == surveyID)
                .Select(ed => new
                {
                    CuestionID = ed.IdPregunta,
                    Option = ed.IdOpcionNavigation
                })
                .ToList();

            int totalValues = 0;

            foreach (var cs in customerSatisfaccion)
            {
                if (cs.CuestionID == 1 && cs.Option.Opcion1 == "NO")
                    return CustomerSatisfaccion.Red;

                totalValues += cs.Option.ValorNumerico != null ? cs.Option.ValorNumerico.Value : 0;
            }

            if(customerSatisfaccion.Count != 0)
            {
                if (totalValues == 6 || totalValues == 7)
                    return CustomerSatisfaccion.Yellow;
                else if (totalValues < 6)
                    return CustomerSatisfaccion.Red;
                else if (totalValues > 7)
                    return CustomerSatisfaccion.Green;
            }

            return CustomerSatisfaccion.Unaswerred;
        }

        public List<ListItemDTO> GetSurveyStates()
        {
            return context.EstadoEncuesta
                    .Select(c => new ListItemDTO
                    {
                        ID = c.Id,
                        Name = c.Nombre
                    }).ToList();
        }   
    }
}
