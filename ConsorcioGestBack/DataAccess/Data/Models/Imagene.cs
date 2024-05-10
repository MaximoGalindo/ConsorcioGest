using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Imagene
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public byte[]? DatosImagen { get; set; }

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
