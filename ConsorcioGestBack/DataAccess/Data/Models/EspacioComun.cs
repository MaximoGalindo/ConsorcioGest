using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EspacioComun
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? HoraDesde { get; set; }

    public string? HoraHasta { get; set; }

    public int? IdConsorcio { get; set; }

    public virtual Consorcio? IdConsorcioNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
