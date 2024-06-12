using BusinessService.Models;
using BusinessService.Services.BaseService;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class StatsService : BaseService<Object>
    {
        private readonly ConsorcioGestContext context;

        public StatsService(ConsorcioGestContext context)
        {
            this.context = context;
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
                 .Take(5)
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

            // Create and return the stats model
            return new StatsModel
            {
                Data = claimsPerMonth
            };
        }

    }
}
