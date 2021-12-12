using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Casting;


namespace CSE_210_FinalProject.Scripting
{
    public class OffScreenAction : Action
    {
        RosterService _rosterService;
        public OffScreenAction(RosterService rosterService)
        {
            _rosterService = rosterService;
        }
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            List<Actor> players = cast["players"];
            List<Actor> bullets = cast["projectiles"];
            List<Actor> playersToRemove = new List<Actor>();
            List<Actor> bulletsToRemove = new List<Actor>();

            foreach (Actor player in players)
            {
                if (IsOffScreen(player))
                {
                    playersToRemove.Add(player);
                    _rosterService.NextTurn(cast);
                }
            }
            foreach (Actor bullet in bullets)
            {
                if (IsOffScreen(bullet))
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (Actor player in playersToRemove)
            {
                cast["players"].Remove(player);
                
            }
            foreach (Actor bullet in bulletsToRemove)
            {
                cast["projectiles"].Remove(bullet);
                _rosterService.NextTurn(cast);
            }
        }

        private bool IsOffScreen(Actor actor)
        {
            bool isOffScreen = false;
            Point position = actor.GetPosition();
            if 
            (
                position.GetX() > Constants.MAX_X ||
                position.GetX() < -Constants.PLAYER_WIDTH ||
                position.GetY() > Constants.MAX_Y
            )
            {
                isOffScreen = true;
            }

            return isOffScreen;
        }
    }
}