using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class ConsorcioUsuario
{
    public int Id { get; set; }

    public int IdConsorcio { get; set; }

    public int IdUsuario { get; set; }

    public virtual Consorcio IdConsorcioNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
