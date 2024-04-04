using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Opcion
{
    public int Id { get; set; }

    public string Opcion1 { get; set; } = null!;

    public int? ValorNumerico { get; set; }

    public virtual ICollection<EncuestasDetalle> EncuestasDetalles { get; set; } = new List<EncuestasDetalle>();
}
