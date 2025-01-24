namespace TerroscapeApp.Models
{
    public static class DBEnums
    {
        public enum GameNameEnum
        {
            Base,
            FeralInstincts,
            AmorphousPeril,
            LethalImmortals,
            PutrefiedEnmity
        }

        public enum WinEnum
        {
            Murder,
            Time,
            Escape,
            Police,
            Plan,
            Other
        }

        /// <summary>
        /// Old version. Please use WinEnum.
        /// </summary>
        public enum KillerWinEnum
        {
            Murder,
            Time,
            Other
        }
        
        /// <summary>
        /// Old version. Please use WinEnum.
        /// </summary>
        public enum SurvivorWinEnum
        {
            Escape,
            Police,
            Plan,
            Other
        }

        public enum SurvivorStateEnum
        {
            Alive,
            Injured,
            Dead
        }
    }
}
