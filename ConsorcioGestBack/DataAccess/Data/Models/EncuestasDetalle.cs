using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EncuestasDetalle
{
    public int Id { get; set; }

    public string? Comentario { get; set; }

    public int IdEncuesta { get; set; }

    public int IdPregunta { get; set; }

    public int IdOpcion { get; set; }

    public virtual Encuesta IdEncuestaNavigation { get; set; } = null!;

    public virtual Opcion IdOpcionNavigation { get; set; } = null!;

    public virtual Preguntum IdPreguntaNavigation { get; set; } = null!;
}
