using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Encuesta
{
    public int Id { get; set; }

    public int IdReclamo { get; set; }

    public int IdEstadoEncuesta { get; set; }

    public int IdConsorcio { get; set; }

    public virtual ICollection<EncuestasDetalle> EncuestasDetalles { get; set; } = new List<EncuestasDetalle>();

    public virtual Consorcio IdConsorcioNavigation { get; set; } = null!;

    public virtual EstadoEncuestum IdEstadoEncuestaNavigation { get; set; } = null!;

    public virtual Reclamo IdReclamoNavigation { get; set; } = null!;
}
