using System;
using System.Collections.Generic;

namespace TerroscapeApp.Database;

public partial class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();

    public virtual ICollection<Survivor> Survivors { get; set; } = new List<Survivor>();
}
