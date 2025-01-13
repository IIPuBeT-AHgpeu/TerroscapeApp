using System;
using System.Collections.Generic;

namespace TerroscapeApp.Database;

public partial class Avatar
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string FirstSkill { get; set; } = null!;

    public string? SecondSkill { get; set; }

    public virtual ICollection<Survivor> Survivors { get; set; } = new List<Survivor>();
}
