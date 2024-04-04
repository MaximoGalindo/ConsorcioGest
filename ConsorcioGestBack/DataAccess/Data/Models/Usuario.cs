using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string Contrasenia { get; set; } = null!;

    public bool Espropietario { get; set; }

    public bool Esinquilino { get; set; }

    public int IdCondominio { get; set; }

    public int IdPerfil { get; set; }

    public int IdEstadoUsuario { get; set; }

    public int IdTipoDocumento { get; set; }

    public virtual ICollection<ConsorcioUsuario> ConsorcioUsuarios { get; set; } = new List<ConsorcioUsuario>();

    public virtual Condominio IdCondominioNavigation { get; set; } = null!;

    public virtual EstadoUsuario IdEstadoUsuarioNavigation { get; set; } = null!;

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
