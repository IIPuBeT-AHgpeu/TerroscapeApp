using System;
using System.Collections.Generic;
using TerroscapeApp.Models;

namespace TerroscapeApp.Database;

public partial class Round
{
    public int Id { get; set; }

    public int MapId { get; set; }

    public int KillerId { get; set; }

    public int KillerPlayerId { get; set; }

    public int KillerBoostNum { get; set; }

    public bool KillerWin { get; set; }

    public int KillerLevel { get; set; }

    public int FirstSurvivor { get; set; }

    public int SecondSurvivor { get; set; }

    public int ThirdSurvivor { get; set; }

    public int SurvivorBoostNum { get; set; }

    public bool HasPlans { get; set; }

    public bool GotKeys { get; set; }

    public bool DoneRadio { get; set; }

    public bool? DonePlan { get; set; }

    public DBEnums.SurvivorWinEnum? HowSurvivorsWin { get; set; }

    public DBEnums.KillerWinEnum? HowKillerWin { get; set; }

    public virtual Survivor FirstSurvivorNavigation { get; set; } = null!;

    public virtual Killer Killer { get; set; } = null!;

    public virtual Player KillerPlayer { get; set; } = null!;

    public virtual Map Map { get; set; } = null!;

    public virtual Survivor SecondSurvivorNavigation { get; set; } = null!;

    public virtual Survivor ThirdSurvivorNavigation { get; set; } = null!;
}
