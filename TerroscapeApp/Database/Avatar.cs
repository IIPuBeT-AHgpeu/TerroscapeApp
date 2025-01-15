using System;
using System.Collections.Generic;
using TerroscapeApp.Models;

namespace TerroscapeApp.Database;

public partial class Avatar
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string FirstSkill { get; set; } = null!;

    public string? SecondSkill { get; set; }

    public DBEnums.GameNameEnum GameName { get; set; } = DBEnums.GameNameEnum.Base;

    public virtual ICollection<Survivor> Survivors { get; set; } = new List<Survivor>();
}
