namespace CSE_210_FinalProject
{
    /// <summary>
    /// This is a set of program wide constants to be used in other classes.
    /// </summary>
    public static class Constants
    {
        public const int MAX_X = 1200;
        public const int MAX_Y = 700;
        public const int FRAME_RATE = 60;

        public const int DEFAULT_SQUARE_SIZE = 20;
        public const int DEFAULT_FONT_SIZE = 20;
        public const int DEFAULT_TEXT_OFFSET = 4;

        public const string TEAM_1_IMAGE = "./Assets/finalWorm.png";
        public const string TEAM_2_IMAGE = "./Assets/armyMan.png";
        public const string ARROW_IMAGE = "./Assets/arrow.png";
        public const string BULLET_IMAGE = "./Assets/bullet.png";
        public const string TEAM_1_GUN_SOUND = "./Assets/rifle.wav";
        public const string TEAM_2_GUN_SOUND = "./Assets/pistol.wav";

        public const int PLAYER_SPEED = 5;
        public const int MAX_MOVEMENT = 20;
        public const int PLAYER_WIDTH = 45;
        public const int PLAYER_HEIGHT = 50;
        public const int AI_HIT_PERCENTAGE = 50;
        public const int TEAM_1_COUNT = 3;
        public const int TEAM_2_COUNT = 3;
        public const int LIFE_COUNT = 3;
        public const int TERRAIN_WIDTH = 1;
        public const int TERRAIN_HEIGHT = 900;
        public const int ARROW_GAP = 40;
        public const int ARROW_WIDTH = 5;
        public const int ARROW_HEIGHT = 5;
        public const int BULLET_WIDTH = 10;
        public const int BULLET_HEIGHT = 10;
        public const int BULLET_SPEED = 3;
        public const int POWER_SCALING = 15;
        public const int BULLET_DAMAGE = 1;
        public const int IMPACT_AREA = 50;
        public const int GRAVITY_RATE = 1;
        public const int GRAVITY_CAP = 20;
        public const int TREND_GAP = 20;
        public const int CLIFF_RATE = 20;

        // Menu Constants
        public const int TEXT_BOX_WIDTH = 200;
        public const int TEXT_BOX_HEIGHT = 30;

        public const string TEAM_1_USER_TEXT = "Team 1 User Count";
        public const string TEAM_2_USER_TEXT = "Team 2 User Count";
        public const string TEAM_2_AI_TEXT = "Team 2 AI Count";
        public const string TEAM_2_AI_DIFFICULTY_TEXT = "AI Difficulty: 0 - Easy, 1 - Medium, 2 - Hard, 3 - Impossible";
        public const string MENU_EXIT_INSTRUCTIONS = "Press 'SPACE' to start game. *Team 2 can have only AI or User players*";
        
        
    }
}