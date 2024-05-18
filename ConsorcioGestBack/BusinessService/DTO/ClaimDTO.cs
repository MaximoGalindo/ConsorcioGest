using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class ClaimDTO
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; }
        public int? StateId { get; set; }
        public string State { get; set;}
        public int? CauseClaimID { get; set; }
        public string CauseClaim { get; set; }
        public string Date {  get; set; }
        public string Condominium { get; set; }
        public int? AffectedSpaceID { get; set; }
        public string AffectedSpace { get; set; }
        public int? UserID { get; set; }
        public string ProblemDetail { get; set; }
    }

    public class HistoryClaimDTO
    {
        public int IdClaim { get; set; }
        public string Observation {  get; set; }
        public string Date { get; set; }
        public string State { get; set; }
    }
}
