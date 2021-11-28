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

            Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();

            cast["players"] = new List<Actor>();
            cast["projectiles"] = new List<Actor>();
            cast["terrain"] = new List<Actor>();
            cast["arrows"] = new List<Actor>();

            // add actors here
            Player player1 = new Player(100,100, true, true);
            cast["players"].Add(player1);

            for (int i = 0; i < Constants.MAX_X; i++)
            {
                Terrain terrain = new Terrain(i,500);
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

            MoveActorsAction moveActorsAction = new MoveActorsAction();
            script["update"].Add(moveActorsAction);
            
            ControlActorsAction controlActorsAction = new ControlActorsAction(inputService);
            script["input"].Add(controlActorsAction);
            
            outputService.OpenWindow(Constants.MAX_X, Constants.MAX_Y, "Worms Armageddon 2.0", Constants.FRAME_RATE);

            Director theDirector = new Director(cast, script);
            theDirector.Direct();
        }
    }
}
