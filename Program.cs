using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Casting;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputService outputService = new OutputService();
            InputService inputService = new InputService();
            PhysicsService physicsService = new PhysicsService();
            RosterService rosterService = new RosterService();
            AudioService audioService = new AudioService();
            

            Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();

            cast["players"] = new List<Actor>();
            cast["projectiles"] = new List<Actor>();
            cast["terrain"] = new List<Actor>();
            cast["arrows"] = new List<Actor>();

            Dictionary<string, List<Actor>> menuCast = new Dictionary<string, List<Actor>>();

            menuCast["instructions"] = new List<Actor>();
            menuCast["inputboxes"] = new List<Actor>();
            
            // Menu actors
            MenuTextBox team1Text = new MenuTextBox(false, Constants.TEAM_1_USER_TEXT);
            team1Text.SetPosition(new Point(10,10));
            menuCast["instructions"].Add(team1Text);


            MenuTextBox team1input = new MenuTextBox(true, "1");
            team1input.SetPosition(new Point(10,50));
            team1input.ChangeIfSelected(true);
            menuCast["inputboxes"].Add(team1input);

            MenuTextBox  team2text = new MenuTextBox(false, Constants.TEAM_2_USER_TEXT);
            team2text.SetPosition(new Point(10,100));
            menuCast["instructions"].Add(team2text);

            MenuTextBox team2input = new MenuTextBox(true, "0");
            team2input.SetPosition(new Point(10,150));
            menuCast["inputboxes"].Add(team2input);

            MenuTextBox  team2AItext = new MenuTextBox(false, Constants.TEAM_2_AI_TEXT);
            team2AItext.SetPosition(new Point(10,200));
            menuCast["instructions"].Add(team2AItext);

            MenuTextBox team2AIinput = new MenuTextBox(true, "0");
            team2AIinput.SetPosition(new Point(10,250));
            menuCast["inputboxes"].Add(team2AIinput);

            MenuTextBox  team2AIDifficultytext = new MenuTextBox(false, Constants.TEAM_2_AI_DIFFICULTY_TEXT);
            team2AIDifficultytext.SetPosition(new Point(10,300));
            menuCast["instructions"].Add(team2AIDifficultytext);

            MenuTextBox team2AIDifficultyinput = new MenuTextBox(true, "0");
            team2AIDifficultyinput.SetPosition(new Point(10,350));
            menuCast["inputboxes"].Add(team2AIDifficultyinput);

            MenuTextBox menuExitInstructions = new MenuTextBox(false, Constants.MENU_EXIT_INSTRUCTIONS);
            menuExitInstructions.SetPosition(new Point(10, Constants.MAX_Y - 100));
            menuCast["instructions"].Add(menuExitInstructions);

            Dictionary<string, List<Action>> menuScript = new Dictionary<string, List<Action>>();

            menuScript["output"] = new List<Action>();
            menuScript["input"] = new List<Action>();
            // menuScript["update"] = new List<Action>();

            // Add Menu Actions here
            UpdateBufferMenuAction updateBufferMenuAction = new UpdateBufferMenuAction(inputService);
            menuScript["input"].Add(updateBufferMenuAction);
            
            DrawActorsAction menuDrawActorsAction = new DrawActorsAction(outputService);
            menuScript["output"].Add(menuDrawActorsAction);

            
            

            
            
            // terrain generation
            List<String> trends = new List<string>{"up", "flat", "down"};
            Random rnd = new Random();
            int y = rnd.Next(Constants.MAX_Y - 300,Constants.MAX_Y - 100);
            int trendnum = rnd.Next(trends.Count);
            string trend = trends[trendnum];

            for (int i = 0; i < Constants.MAX_X + 1; i++)
            {
                if (i % Constants.TREND_GAP == 0)
                {
                    if ((int)rnd.Next(0,3) != 1)
                    {
                        trendnum = rnd.Next(trends.Count);
                        trend = trends[trendnum];
                    }
                    // Cliff down
                    if (rnd.Next(0,Constants.CLIFF_RATE) == 1)
                    {
                        y -= 100;
                    }
                    // cliff up
                    else if (rnd.Next(0,Constants.CLIFF_RATE) == 1)
                    {
                        y += 100;
                    }
                }

                if (trend == "up")
                {
                    y -= 1;
                }
                else if (trend == "down")
                {
                    y ++;
                }
                
                if (y <= 300)
                {
                    trend = "down";
                }
                else if (y >= Constants.MAX_Y - 50)
                {
                    trend = "up";
                }
                // rough terrain
                y += rnd.Next(-1,2);
                
                Terrain terrain = new Terrain(i,y);
                cast["terrain"].Add(terrain);
            }


            Arrow arrow = new Arrow();
            cast["arrows"].Add(arrow);

            Dictionary<string, List<Action>> script = new Dictionary<string, List<Action>>();

            script["output"] = new List<Action>();
            script["input"] = new List<Action>();
            script["update"] = new List<Action>();

            

            // add Game actions here
            DrawActorsAction drawActorsAction = new DrawActorsAction(outputService);
            script["update"].Add(drawActorsAction);

            GravityAction gravityAction = new GravityAction();
            script["update"].Add(gravityAction);

            HandleCollisionsAction handleCollisionsAction = new HandleCollisionsAction(physicsService, rosterService);
            script["update"].Add(handleCollisionsAction);

            OffScreenAction offScreenAction = new OffScreenAction(rosterService);
            script["update"].Add(offScreenAction);

            MoveActorsAction moveActorsAction = new MoveActorsAction();
            script["update"].Add(moveActorsAction);
            
            ControlActorsAction controlActorsAction = new ControlActorsAction(inputService, rosterService);
            script["input"].Add(controlActorsAction);

            MakeShotAction makeShotAction = new MakeShotAction(rosterService, inputService, physicsService, audioService);
            script["input"].Add(makeShotAction);
            
            
            outputService.OpenWindow(Constants.MAX_X, Constants.MAX_Y, "Worms Armageddon 2.0", Constants.FRAME_RATE);

            Menu menu = new Menu(menuCast, menuScript);
            menu.OpenMenu();

            int team1Count = int.Parse(team1input.GetText());
            int team2UserCount = int.Parse(team2input.GetText());
            int team2AICount = int.Parse(team2AIinput.GetText());
            int team2Count = team2UserCount + team2AICount;
            int difficulty = int.Parse(team2AIDifficultyinput.GetText());
            int AIdifficulty = 0;
            if (difficulty == 0)
            {
                AIdifficulty = 20;
            }
            else if (difficulty == 1)
            {
                AIdifficulty = 40;
            }
            else if (difficulty == 2)
            {
                AIdifficulty = 65;
            }
            else
            {
                AIdifficulty = 100;
            }
            bool is2User = (team2UserCount > 0);
            
            
            // Main Game loop actors
            for (int i = 1; i < team1Count + 1; i++)
            {
                Player player1 = new Player(((Constants.MAX_X / 2) / team1Count + 1) * i - 50, 0, true, 1);
                if (i == 1)
                {
                    player1.SetActivity(true);
                }
                player1.SetImage(Constants.TEAM_1_IMAGE);
                cast["players"].Add(player1);
            }

            for (int i = 1; i < team2Count + 1; i++)
            {
                Player player2 = new Player((((Constants.MAX_X / 2) / team2Count + 1) * i ) + (Constants.MAX_X / 2) - 50, 0, is2User, 2);
                player2.SetImage(Constants.TEAM_2_IMAGE);
                cast["players"].Add(player2);
                
                if (!is2User)
                {
                    player2.SetAIdifficulty(AIdifficulty);
                }
            }
            audioService.StartAudio();

            Director theDirector = new Director(cast, script, rosterService);
            theDirector.Direct();

            audioService.StopAudio();
        }
    }
}
