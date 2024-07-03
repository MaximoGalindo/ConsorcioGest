using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public bool? Espropietario { get; set; }

    public bool? Esinquilino { get; set; }

    public int? IdCondominio { get; set; }

    public int? IdPerfil { get; set; }

    public int? IdEstadoUsuario { get; set; }

    public int? IdTipoDocumento { get; set; }

    public int Documento { get; set; }

    public DateTime? ActivationDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual ICollection<ConsorcioUsuario> ConsorcioUsuarios { get; set; } = new List<ConsorcioUsuario>();

    public virtual Condominio? IdCondominioNavigation { get; set; }

    public virtual EstadoUsuario? IdEstadoUsuarioNavigation { get; set; }

    public virtual Perfil? IdPerfilNavigation { get; set; }

    public virtual TipoDocumento? IdTipoDocumentoNavigation { get; set; }

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
