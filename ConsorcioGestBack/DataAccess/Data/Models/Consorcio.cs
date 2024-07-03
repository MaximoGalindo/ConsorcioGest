using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Consorcio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string? Cuit { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual ICollection<Condominio> Condominios { get; set; } = new List<Condominio>();

    public virtual ICollection<ConsorcioUsuario> ConsorcioUsuarios { get; set; } = new List<ConsorcioUsuario>();

    public virtual ICollection<ConsortiumConfiguration> ConsortiumConfigurations { get; set; } = new List<ConsortiumConfiguration>();

    public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();

    public virtual ICollection<Encuesta> Encuesta { get; set; } = new List<Encuesta>();

    public virtual ICollection<EspacioComunConsorcio> EspacioComunConsorcios { get; set; } = new List<EspacioComunConsorcio>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
