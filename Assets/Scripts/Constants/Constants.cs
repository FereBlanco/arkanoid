namespace Scripts.Game
{
    public static class Constants
    {
        public static string HORIZONTAL_AXIS = "Horizontal";
        public static string FIRE_AXIS = "Jump";

        public static int MAX_LIVES = 10;
        public static int[] EXTRA_LIFE_COSTS = {20000, 40000, 60000};

        public static string TAG_VAUS = "Vaus";
        public static string TAG_BALL = "Ball";
        public static string TAG_BRICK = "Brick";

    }

    public enum PowerUpType
    {
        None = -1, Break = 0, Catch = 1, Disruption = 2, Enlarge = 3, Laser = 4, Player = 5, Slow = 6
    }        
}