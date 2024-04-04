using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? HoraDesde { get; set; }

    public string? HoraHasta { get; set; }

    public int? IdConsorcio { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEstadoReserva { get; set; }

    public int? IdEspacioComun { get; set; }

    public virtual Consorcio? IdConsorcioNavigation { get; set; }

    public virtual EspacioComun? IdEspacioComunNavigation { get; set; }

    public virtual EstadoReserva? IdEstadoReservaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
