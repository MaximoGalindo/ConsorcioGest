using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class CommonSpaceModel
    {
        public int Id { get; set; }
        public int UserLimit { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set;}
        public int NumberReservationsAvailable { get; set; }
        public int CommonSpaceID { get; set; }
        public string CommonSpaceName { get; set; } 
    }
}
