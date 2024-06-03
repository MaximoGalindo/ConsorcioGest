using BusinessService.DTO;
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
        public UserModelDTO User { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set;}
        public DateTime Date { get; set; }
        public string StateReservation { get; set; }
        public int CommonSpaceConsortiumID { get; set; }
    }

    public class ReservationUser
    {
        public int Id { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public DateTime Date { get; set; }
        public int StateReservationID { get; set; }
        public string StateReservation { get; set; }
        public int CommonSpaceConsortiumID { get; set; }
    }
}
