using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class ConsortiumConfiguration
{
    public int Id { get; set; }

    public string DeparmentConfiguration { get; set; } = null!;

    public string TowerName { get; set; } = null!;

    public int Floors { get; set; }

    public string CountDepartmentsByFloor { get; set; } = null!;

    public int IdConsortium { get; set; }

    public virtual Consorcio IdConsortiumNavigation { get; set; } = null!;
}
