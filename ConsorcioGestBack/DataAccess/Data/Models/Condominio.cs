using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Condominio
{
    public int Id { get; set; }

    public string Torre { get; set; } = null!;

    public string Piso { get; set; } = null!;

    public string Departamento { get; set; } = null!;

    public int IdConsorcio { get; set; }

    public virtual Consorcio IdConsorcioNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
