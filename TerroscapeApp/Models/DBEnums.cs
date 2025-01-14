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

        public enum KillerWinEnum
        {
            Murder,
            Time,
            Other
        }

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
