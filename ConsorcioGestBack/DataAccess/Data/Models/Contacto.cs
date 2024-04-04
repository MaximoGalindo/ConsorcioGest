using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Contacto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public int IdConsorcio { get; set; }

    public virtual Consorcio IdConsorcioNavigation { get; set; } = null!;
}
