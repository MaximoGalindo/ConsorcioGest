using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class ReclamoDetalle
{
    public int Id { get; set; }

    public string? Observacion { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public int? IdReclamo { get; set; }

    public virtual Reclamo? IdReclamoNavigation { get; set; }
}
