using System;
using System.Collections.Generic;

namespace TerroscapeApp.Database;

public partial class Killer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Strength { get; set; }

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
