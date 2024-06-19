using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class BaseFilterClaimDTO
    {
        public int CauseClaim { get; set; }
        public string? NroReclamo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; } = DateTime.Now.Date;
    }

    public class FilterClaimDTO : BaseFilterClaimDTO
    {
        public int StateID { get; set; }
    }

    public class FilterClaimUserDTO : BaseFilterClaimDTO
    {
        
    }
}
