using System;
using System.Collections.Generic;
using TerroscapeApp.Models;

namespace TerroscapeApp.Database;

public partial class Killer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Strength { get; set; }

    public DBEnums.GameNameEnum GameName { get; set; } = DBEnums.GameNameEnum.Base;

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
