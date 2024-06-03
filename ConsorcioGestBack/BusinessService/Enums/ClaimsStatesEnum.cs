using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Enums
{
    public enum ClaimsStatesEnum
    {
        PENDING = 1,
        IN_PROGRESS = 2,
        CANCELLED = 3,
        FINISHED = 4,
    }

    public enum UserStatesEnum
    {
        Habilitado = 1,
        Deshabilitado = 2,
        PendienteDeAprobacion = 3,
        Rechazado = 4
    }
    
    public enum ReservationsStatesEnum
    {
        RESERVATED = 1,
        CANCELLED = 2,
        FINISHED = 3
    }

    public enum SurveyStatesEnum 
    {
        COMPLETED = 1,
        INITIATED = 2,
    }
}
