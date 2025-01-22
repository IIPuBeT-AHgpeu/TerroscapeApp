using System;
using System.Collections.Generic;

namespace TerroscapeApp.Database;

public partial class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public virtual ICollection<Round> KillerRounds { get; set; } = new List<Round>();
    public virtual ICollection<Round> FirstSurvivorRounds { get; set; } = new List<Round>();
    public virtual ICollection<Round> SecondSurvivorRounds { get; set; } = new List<Round>();
    public virtual ICollection<Round> ThirdSurvivorRounds { get; set; } = new List<Round>();
}
