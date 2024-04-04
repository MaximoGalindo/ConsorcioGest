using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EstadoEncuestum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Observacion { get; set; }

    public virtual ICollection<Encuesta> Encuesta { get; set; } = new List<Encuesta>();
}
