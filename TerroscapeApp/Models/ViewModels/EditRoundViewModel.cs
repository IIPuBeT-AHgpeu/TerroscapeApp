using TerroscapeApp.Database;

namespace TerroscapeApp.Models.ViewModels
{
    public class EditRoundViewModel : AddRoundViewModel
    {
        public int Id { get; set; }

        public static explicit operator Round(EditRoundViewModel model)
        {
            var addRoundModel = model as AddRoundViewModel;

            var round = (Round)addRoundModel;

            round.Id = model.Id;

            return round;
        }

        public static explicit operator EditRoundViewModel(Round round)
        {
            var editRound = new EditRoundViewModel();

            editRound.Id = round.Id;
            editRound.MapId = round.MapId;
            editRound.KillerId = round.KillerId;
            editRound.KillerPlayerId = round.KillerPlayerId;
            editRound.KillerBoostNum = round.KillerBoostNum;
            editRound.KillerWin = round.KillerWin;
            editRound.KillerLevel = round.KillerLevel;
            editRound.SurvivorBoostNum = round.SurvivorBoostNum;
            editRound.HasPlans = round.HasPlans;
            if (round.DonePlan == null) editRound.DonePlan = false;
            else editRound.DonePlan = (bool)round.DonePlan;
            editRound.GotKeys = round.GotKeys;
            editRound.DoneRadio = round.DoneRadio;
            editRound.FirstPlayerId = round.FirstPlayerId;
            editRound.FirstAvatarId = round.FirstAvatarId;
            editRound.FirstState = round.FirstState;
            if (round.SecondPlayerId == null) editRound.SecondPlayerId = 0;
            else editRound.SecondPlayerId = (int)round.SecondPlayerId;
            editRound.SecondAvatarId = round.SecondAvatarId;
            editRound.SecondState = round.SecondState;
            if (round.ThirdPlayerId == null) editRound.ThirdPlayerId = 0;
            else editRound.ThirdPlayerId = (int)round.ThirdPlayerId;
            editRound.ThirdAvatarId = round.ThirdAvatarId;
            editRound.ThirdState = round.ThirdState;
            editRound.WinWay = round.WinWay;
            editRound.Date = round.Date;

            return editRound;
        }
    }
}
