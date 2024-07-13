using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class BaseFilter 
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; } = DateTime.Now.Date;
    }

    public class BaseFilterClaimDTO : BaseFilter
    {
        public int CauseClaim { get; set; }
        public string? NroReclamo { get; set; }
    }

    public class FilterClaimDTO : BaseFilterClaimDTO
    {
        public int StateID { get; set; }
    }

    public class FilterClaimUserDTO : BaseFilterClaimDTO
    {

    }

    public class BaseFilterReservationsDTO : BaseFilter
    {
        public int CommonSpaceID { get; set; }
    }

    public class FilterReservationDTO : BaseFilterReservationsDTO 
    {
        public int Document {  get; set; }
    }

    public class FilterReservationUserDTO : BaseFilterReservationsDTO
    {

    }

    public class FilterUserDTO
    {
        public int Document { get; set; }
        public string? Tower { get; set; }
        public string? Condominium { get; set; }
        public int UserStateID { get; set; }
    }

    public class FilterSurveyDTO : BaseFilter 
    {
        public int StateID { get; set; }
        public string? ClaimNumber { get; set; }
    }



}
