using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EspacioComunConsorcio
{
    public int Id { get; set; }

    public int LimiteUsuarios { get; set; }

    public string HoraDesde { get; set; } = null!;

    public string HoraHasta { get; set; } = null!;

    public int CantidadReservasDisponibles { get; set; }

    public int IdConsorcio { get; set; }

    public int IdEspacioComun { get; set; }

    public bool? Activo { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual Consorcio IdConsorcioNavigation { get; set; } = null!;

    public virtual EspacioComun IdEspacioComunNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
