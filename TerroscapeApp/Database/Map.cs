using System;
using System.Collections.Generic;

namespace TerroscapeApp.Database;

public partial class Map
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LocationsNum { get; set; }

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
