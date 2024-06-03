using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class PreguntaOpcione
{
    public int Id { get; set; }

    public int IdPregunta { get; set; }

    public int IdOpcion { get; set; }

    public virtual Opcion IdOpcionNavigation { get; set; } = null!;

    public virtual Preguntum IdPreguntaNavigation { get; set; } = null!;
}
