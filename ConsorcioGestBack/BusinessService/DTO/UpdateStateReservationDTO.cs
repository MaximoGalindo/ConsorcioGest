﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class UpdateStateReservationDTO
    {
        public int ReservationID { get; set; }
        public int StateReservationID { get; set; }
        public string Message { get; set; }
    }

    public class UpdateStateCommonSpaceDTO 
    {
        public int CommonSpaceID { get; set; }
        public bool State { get; set; }
    }
}
