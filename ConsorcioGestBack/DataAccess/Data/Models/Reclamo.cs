using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Reclamo
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Comentario { get; set; }

    public string? NroReclamo { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEstadoReclamo { get; set; }

    public int? IdEspacioAfectado { get; set; }

    public int? IdCausaProblema { get; set; }

    public virtual ICollection<Encuesta> Encuesta { get; set; } = new List<Encuesta>();

    public virtual CausaProblema? IdCausaProblemaNavigation { get; set; }

    public virtual EspacioAfectado? IdEspacioAfectadoNavigation { get; set; }

    public virtual EstadoReclamo? IdEstadoReclamoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<ReclamoDetalle> ReclamoDetalles { get; set; } = new List<ReclamoDetalle>();
}
