using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class ReservationDTO
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public int CommonSpaceConsortiumID { get; set; }
    }

    public class SchedulesAvailableDTO 
    {
        public string Hour { get; set;}
        public bool Available { get; set; }
    }

}
