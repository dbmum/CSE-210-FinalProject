using System;
using System.Collections.Generic;

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

            Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();

            cast["players"] = new List<Actor>();
            cast["projectiles"] = new List<Actor>();
            cast["terrain"] = new List<Actor>();
            cast["arrows"] = new List<Actor>();

            

            for (int i = 1; i < Constants.TEAM_1_COUNT + 1; i++)
            {
                Player player1 = new Player(((Constants.MAX_X / 2) / Constants.TEAM_1_COUNT + 1) * i - 50, 0, true, 1);
                if (i == 1)
                {
                    player1.SetActivity(true);
                }
                player1.SetImage("./Assets/brick-2.png");
                cast["players"].Add(player1);
            }

            for (int i = 1; i < Constants.TEAM_2_COUNT + 1; i++)
            {
                Player player2 = new Player((((Constants.MAX_X / 2) / Constants.TEAM_2_COUNT + 1) * i ) + (Constants.MAX_X / 2) - 50, 0, true, 2);
                player2.SetImage("./Assets/brick-1.png");
                cast["players"].Add(player2);
            }
            
            
            List<String> trends = new List<string>{"up", "flat", "down"};
            Random rnd = new Random();
            int y = rnd.Next(Constants.MAX_Y - 300,Constants.MAX_Y - 200);
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
                        y -= 50;
                    }
                    // cliff up
                    else if (rnd.Next(0,Constants.CLIFF_RATE) == 1)
                    {
                        y += 50;
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
                
                if (y <= 50)
                {
                    trend = "down";
                }
                else if (y >= Constants.MAX_Y - Constants.PLAYER_HEIGHT)
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

            // add actions here
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

            MakeShotAction makeShotAction = new MakeShotAction(rosterService, inputService);
            script["input"].Add(makeShotAction);
            
            
            outputService.OpenWindow(Constants.MAX_X, Constants.MAX_Y, "Worms Armageddon 2.0", Constants.FRAME_RATE);

            Director theDirector = new Director(cast, script, rosterService);
            theDirector.Direct();
        }
    }
}
