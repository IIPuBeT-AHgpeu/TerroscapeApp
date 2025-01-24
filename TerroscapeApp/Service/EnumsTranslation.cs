using TerroscapeApp.Models;

namespace TerroscapeApp.Service
{
    public static class EnumsTranslation
    {
        public static Dictionary<Enum, string> GameNameTranslation { get; private set; }
        public static Dictionary<Enum, string> WinWayTranslation { get; private set; }
        public static Dictionary<Enum, string> SurvivorStateTranslation { get; private set; }

        static EnumsTranslation()
        {
            GameNameTranslation = new Dictionary<Enum, string>
            {
                { DBEnums.GameNameEnum.Base, "База" },
                { DBEnums.GameNameEnum.FeralInstincts, "Животный инстинкт" },
                { DBEnums.GameNameEnum.AmorphousPeril, "Угроза извне" },
                { DBEnums.GameNameEnum.LethalImmortals, "Безжизненные бессмертные" },
                { DBEnums.GameNameEnum.PutrefiedEnmity, "Королева мертвых" }
            };

            WinWayTranslation = new Dictionary<Enum, string>()
            {
                { DBEnums.KillerWinEnum.Murder, "Убийство жертвы" },
                { DBEnums.KillerWinEnum.Time, "Закончились карты экипировки" },
                { DBEnums.SurvivorWinEnum.Escape, "Побег" },
                { DBEnums.SurvivorWinEnum.Police, "Прибытие полиции" },
                { DBEnums.SurvivorWinEnum.Plan, "Выполнен победный план" },
                { DBEnums.KillerWinEnum.Other, "Другое" }
            };

            SurvivorStateTranslation = new Dictionary<Enum, string>()
            {
                { DBEnums.SurvivorStateEnum.Alive, "Выжил" },
                { DBEnums.SurvivorStateEnum.Injured, "Ранен" },
                { DBEnums.SurvivorStateEnum.Dead, "Погиб" }
            };
        }
    }
}
