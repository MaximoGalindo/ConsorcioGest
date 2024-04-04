using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EstadoReserva
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Observacion { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
