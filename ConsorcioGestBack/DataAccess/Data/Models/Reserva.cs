using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public string HoraDesde { get; set; } = null!;

    public string HoraHasta { get; set; } = null!;

    public int IdConsorcio { get; set; }

    public int IdUsuario { get; set; }

    public int IdEstadoReserva { get; set; }

    public int IdEspacioComunConsorcio { get; set; }

    public virtual Consorcio IdConsorcioNavigation { get; set; } = null!;

    public virtual EspacioComunConsorcio IdEspacioComunConsorcioNavigation { get; set; } = null!;

    public virtual EstadoReserva IdEstadoReservaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
