using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ReservationModel
    {
        public int Id { get; set; } 
        public int UserID { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set;}
        public DateOnly Date { get; set; }
        public int StateReservationID { get; set; }
        public string StateReservation { get; set; }
        public int CommonSpaceConsortiumID { get; set; }
    }
}
