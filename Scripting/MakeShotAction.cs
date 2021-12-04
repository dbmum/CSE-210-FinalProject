using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class MakeShotAction : Action
    {
        RosterService _rosterService;
        InputService _inputService;
        public MakeShotAction(RosterService rosterService, InputService inputService)
        {
            _rosterService = rosterService;
            _inputService = inputService;
        }
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            Player activePlayer = _rosterService.GetActivePlayer(cast);

            if (_inputService.IsMouseLeftClick() && !activePlayer.HasShot())
            {
                int mouseX = _inputService.GetMouseX();
                int mouseY = _inputService.GetMouseY();

                int activePlayerX = activePlayer.GetPosition().GetX();
                int activePlayerY = activePlayer.GetPosition().GetY();

                int dx = mouseX - activePlayerX;
                int dy = mouseY - activePlayerY;

                double hyp =  Math.Sqrt((Math.Pow(dx,2)) + (Math.Pow(dy,2)));

                double velx = (((Constants.BULLET_SPEED + (hyp / Constants.POWER_SCALING) ) / hyp )* dx);
                double vely =(((Constants.BULLET_SPEED + (hyp / Constants.POWER_SCALING)) / hyp )* dy);
                
                


                Point bulletVelocity = new Point((int)velx, (int)vely);
                Bullet bullet = new Bullet(activePlayerX, activePlayerY - 10, bulletVelocity);
                cast["projectiles"].Add(bullet);

                activePlayer.SetHasShot(true);
                Point stop = new Point(0,0);
                activePlayer.SetVelocity(stop);
            }


        }
    }
}