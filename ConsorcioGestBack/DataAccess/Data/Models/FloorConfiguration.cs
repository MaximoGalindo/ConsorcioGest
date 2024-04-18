using System;
using System.Collections.Generic;

namespace DataAccess.Data.Models;

public partial class FloorConfiguration
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public int DeparmentCount { get; set; }

    public virtual ICollection<ConsortiumConfiguration> ConsortiumConfigurations { get; set; } = new List<ConsortiumConfiguration>();
}
