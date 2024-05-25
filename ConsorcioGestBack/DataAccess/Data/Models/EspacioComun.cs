using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class EspacioComun
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<EspacioComunConsorcio> EspacioComunConsorcios { get; set; } = new List<EspacioComunConsorcio>();
}
