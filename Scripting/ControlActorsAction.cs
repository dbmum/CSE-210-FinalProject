using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    ///<summary>An action to interpret inputs into moving the paddle</summary>
    public class ControlActorsAction : Action
    {
        InputService _inputService;
        Player _activePlayer;
        public ControlActorsAction(InputService inputService)
        {
            _inputService = inputService;
        }

        ///<summary>Use input to set paddle velocity, and handle its wall collision</summary>
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            Point direction = _inputService.GetDirection();
            Actor arrow = cast["arrows"][0];
            
            GetActivePlayer(cast);

            Point arrowPosition = new Point(_activePlayer.GetPosition().GetX() 
                + (Constants.PLAYER_WIDTH  / 4),
                _activePlayer.GetPosition().GetY() - Constants.ARROW_GAP);
            
            arrow.SetPosition(arrowPosition);
                         
            if ((_inputService.IsLeftPressed() || _inputService.IsRightPressed()) && _activePlayer.isUser())
            {
                Point velocity = direction.Scale(Constants.PLAYER_SPEED);
                _activePlayer.SetVelocity(velocity);

            }
            else 
            {
                StopActor(_activePlayer);
            }    
        }

        ///<summary>Return actor velocity to 0 so that it does not move continuously</summary>
        ///<param name="actor actor">
        private void StopActor(Actor actor)
        {
            Point velocity = new Point(0,0);
            actor.SetVelocity(velocity);
        }

        private void GetActivePlayer(Dictionary<string, List<Actor>> cast)
        {
            foreach (Player player in cast["players"])
            {
                bool isActive = player.GetStatus();                
                if (isActive)
                {
                    _activePlayer = player;
                    break;
                }

            }
        }
    }
}