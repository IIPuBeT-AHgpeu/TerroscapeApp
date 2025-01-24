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

    public int SurvivorBoostNum { get; set; }

    public bool HasPlans { get; set; }

    public bool GotKeys { get; set; }

    public bool DoneRadio { get; set; }

    public bool? DonePlan { get; set; }

    public int FirstPlayerId { get; set; }
    public int FirstAvatarId { get; set; }
    public DBEnums.SurvivorStateEnum FirstState { get; set; } = DBEnums.SurvivorStateEnum.Alive;

    public int? SecondPlayerId { get; set; }
    public int SecondAvatarId { get; set; }
    public DBEnums.SurvivorStateEnum SecondState { get; set; } = DBEnums.SurvivorStateEnum.Alive;

    public int? ThirdPlayerId { get; set; }
    public int ThirdAvatarId { get; set; }
    public DBEnums.SurvivorStateEnum ThirdState { get; set; } = DBEnums.SurvivorStateEnum.Alive;

    public DBEnums.WinEnum WinWay { get; set; } = DBEnums.WinEnum.Murder;

    public DateTime? Date { get; set; }

    public virtual Killer Killer { get; set; } = null!;

    public virtual Player KillerPlayer { get; set; } = null!;

    public virtual Map Map { get; set; } = null!;

    public virtual Player FirstPlayer { get; set; } = null!;

    public virtual Player? SecondPlayer { get; set; }

    public virtual Player? ThirdPlayer { get; set; }

    public virtual Avatar FirstAvatar { get; set; } = null!;
    public virtual Avatar SecondAvatar { get; set; } = null!;
    public virtual Avatar ThirdAvatar { get; set; } = null!;

}
