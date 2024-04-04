using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Observacion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
