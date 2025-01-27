using TerroscapeApp.Database;

namespace TerroscapeApp.Models.ViewModels
{
    public class AddRoundViewModel
    {
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

        public bool DonePlan { get; set; }

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

        public static explicit operator Round(AddRoundViewModel model)
        {
            var round = new Round();

            round.MapId = model.MapId;
            round.KillerId = model.KillerId;
            round.KillerPlayerId = model.KillerPlayerId;
            round.KillerBoostNum = model.KillerBoostNum;
            round.KillerWin = model.KillerWin;
            round.KillerLevel = model.KillerLevel;
            round.SurvivorBoostNum = model.SurvivorBoostNum;
            round.HasPlans = model.HasPlans;
            if (model.HasPlans) round.DonePlan = model.DonePlan;
            else round.DonePlan = null;
            round.GotKeys = model.GotKeys;
            round.DoneRadio = model.DoneRadio;
            round.FirstPlayerId = model.FirstPlayerId;
            round.FirstAvatarId = model.FirstAvatarId;
            round.FirstState = model.FirstState;
            if (model.SecondPlayerId == 0) round.SecondPlayerId = null; 
            else round.SecondPlayerId = model.SecondPlayerId;
            round.SecondAvatarId = model.SecondAvatarId;
            round.SecondState = model.SecondState;
            if (model.ThirdPlayerId == 0) round.ThirdPlayerId = null; 
            else round.ThirdPlayerId = model.ThirdPlayerId;
            round.ThirdAvatarId = model.ThirdAvatarId;
            round.ThirdState = model.ThirdState;
            round.WinWay = model.WinWay;
            if (model.Date == null) round.Date = DateTime.Now;
            else round.Date = model.Date;

            return round;
        }
    }
}
