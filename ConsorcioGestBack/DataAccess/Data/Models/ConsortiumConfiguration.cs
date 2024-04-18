using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class ConsortiumConfiguration
{
    public int Id { get; set; }

    public string FloorConfiguration { get; set; } = null!;

    public string DeparmentConfiguration { get; set; } = null!;

    public string TowerName { get; set; } = null!;

    public int IdFloor { get; set; }

    public int IdConsortium { get; set; }

    public virtual Consorcio IdConsortiumNavigation { get; set; } = null!;

    public virtual FloorConfiguration IdFloorNavigation { get; set; } = null!;
}
