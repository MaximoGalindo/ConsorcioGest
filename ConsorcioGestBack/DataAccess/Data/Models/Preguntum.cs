using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Preguntum
{
    public int Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public virtual ICollection<EncuestasDetalle> EncuestasDetalles { get; set; } = new List<EncuestasDetalle>();
}
