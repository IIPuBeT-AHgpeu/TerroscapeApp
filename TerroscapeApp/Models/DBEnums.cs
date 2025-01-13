namespace TerroscapeApp.Models
{
    public static class DBEnums
    {
        public enum GameNameEnum
        {
            Base = 0,
            Feral_instincts = 1,
            Amorphous_peril = 2,
            Lethal_immortals = 3,
            Putrefied_enmity = 4
        }

        public enum KillerWinEnum
        {
            Murder = 0,
            Time = 1,
            Other = 2
        }

        public enum SurvivorWinEnum
        {
            Escape = 0,
            Police = 1,
            Plan = 2,
            Other = 3
        }

        public enum SurvivorStateEnum
        {
            Alive = 0,
            Injured = 1,
            Dead = 2
        }
    }
}
