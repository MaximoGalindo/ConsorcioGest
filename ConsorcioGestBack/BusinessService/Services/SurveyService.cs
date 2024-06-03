﻿using BusinessService.DTO;
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
            var encuestaExist = context.Encuestas.Any(e => e.IdReclamo == claimID);

            if(!encuestaExist)
            {
                Encuesta encuesta = new Encuesta
                {
                    IdReclamo = claimID,
                    IdEstadoEncuesta = (int)SurveyStatesEnum.INITIATED,
                };
                DBAdd(encuesta, context);
            }

            await SendSurveyByEmail(claimID);
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
            DBUpdate(encuesta, context);
            return true;
        }

        public async Task<bool> SendSurveyByEmail(int claimID)
        {
            var surveyID = context.Encuestas.Where(e => e.IdReclamo == claimID).Select(e => e.Id);
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

    }
}
