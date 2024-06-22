using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Services.BaseService;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class StatsService : BaseService<Object>
    {
        private readonly ConsorcioGestContext context;
        private readonly SurveyService surveyService;

        public StatsService(ConsorcioGestContext context, SurveyService surveyService)
        {
            this.context = context;
            this.surveyService = surveyService;
        }

        public StatsModel GetMostFrequentComplaintsByCauseOfComplaint(DateTime? dateFrom, DateTime? dateTo)
        {
            var reclamos = context.Reclamos
                .Include(r => r.IdCausaProblemaNavigation)
                .Where(r => r.IdUsuarioNavigation.ConsorcioUsuarios.Any(cu => cu.IdConsorcio == LoginService.CurrentConsortium.Id))
                .ToList();

            if (dateFrom.HasValue)
            {
                reclamos = reclamos.Where(r => r.Fecha >= dateFrom.Value).ToList();
            }

            if (dateTo.HasValue)
            {
                reclamos = reclamos.Where(r => r.Fecha <= dateTo.Value).ToList();
            }

            var groupedComplaints = reclamos
                 .GroupBy(r => r.IdCausaProblemaNavigation)
                 .Select(g => new
                 {
                     CauseOfComplaint = g.Key.Nombre,
                     Count = g.Count()
                 })
                 .OrderByDescending(g => g.Count)
                 .Take(4)
                 .ToDictionary(g => g.CauseOfComplaint, g => g.Count);

            return new StatsModel
            {
                Data = groupedComplaints
            };

        }

        public StatsModel GetNumberOfClaimsPerMonths()
        {
            var reclamos = context.Reclamos
               .Where(r => r.IdUsuarioNavigation.ConsorcioUsuarios.Any(cu => cu.IdConsorcio == LoginService.CurrentConsortium.Id))
               .ToList();

            var claimsPerMonth = new Dictionary<string, int>
            {
                { "January", 0 },
                { "February", 0 },
                { "March", 0 },
                { "April", 0 },
                { "May", 0 },
                { "June", 0 },
                { "July", 0 },
                { "August", 0 },
                { "September", 0 },
                { "October", 0 },
                { "November", 0 },
                { "December", 0 }
            };

            foreach (var reclamo in reclamos)
            {
                var month = reclamo.Fecha.Value.Month;
                var monthName = new DateTime(1, month, 1).ToString("MMMM");
                claimsPerMonth[monthName]++;
            }

            return new StatsModel
            {
                Data = claimsPerMonth
            };
        }

        public StatsModel GetNumberOfGestionClaimsPerMonth(int month, int year)
        {
            var encuestas = context.Encuestas
               .Where(r => r.IdConsorcio == 6 && r.Fecha.Value.Month == month && r.Fecha.Value.Year == year)
               .ToList();

            var gestionClaims = new Dictionary<string, int>
            {
                {"Satisfechos", 0 },
                {"Regular", 0 },
                {"Insatisfechos", 0 }
            };

            foreach (var encuesta in encuestas)
            {
                var result = surveyService.GetCustomerSatisfaccion(encuesta.Id);

                switch (result)
                {
                    case CustomerSatisfaccion.Green:
                        gestionClaims["Satisfechos"]++;
                        break;
                    case CustomerSatisfaccion.Yellow:
                        gestionClaims["Regular"]++;
                        break;
                    case CustomerSatisfaccion.Red:
                        gestionClaims["Insatisfechos"]++;
                        break;
                }
            }

            return new StatsModel
            {
                Data = gestionClaims
            };
        }


        public List<int> GetYearsWithClaims()
        {
            var currentYear = DateTime.Now.Year;
            var pastTenYears = Enumerable.Range(currentYear - 10, 11);

            var yearsWithClaims = context.Reclamos
                .Where(r => pastTenYears.Contains(r.Fecha.Value.Year))
                .Select(r => r.Fecha.Value.Year)
                .Distinct()
                .OrderBy(year => year)
                .ToList();

            return yearsWithClaims;
        }


        //public List<StatsModel> Get

    }
}
