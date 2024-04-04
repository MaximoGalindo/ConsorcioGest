using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EspacioAfectado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Observacion { get; set; }

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
