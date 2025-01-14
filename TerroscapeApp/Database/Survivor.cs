using System;
using System.Collections.Generic;
using TerroscapeApp.Models;

namespace TerroscapeApp.Database;

public partial class Survivor
{
    public int Id { get; set; }

    public int? PlayerId { get; set; }

    public int AvatarId { get; set; }

    public DBEnums.SurvivorStateEnum State { get; set; }

    public virtual Avatar Avatar { get; set; } = null!;

    public virtual Player? Player { get; set; }

    public virtual ICollection<Round> RoundFirstSurvivorNavigations { get; set; } = new List<Round>();

    public virtual ICollection<Round> RoundSecondSurvivorNavigations { get; set; } = new List<Round>();

    public virtual ICollection<Round> RoundThirdSurvivorNavigations { get; set; } = new List<Round>();
}
