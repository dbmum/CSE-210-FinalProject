using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    ///<summary>An action to interpret inputs into moving the paddle</summary>
    public class ControlActorsAction : Action
    {
        InputService _inputService;
        RosterService _rosterservice;
        public ControlActorsAction(InputService inputService, RosterService rosterService)
        {
            _inputService = inputService;
            _rosterservice = rosterService;
        }

        ///<summary>Use input to set paddle velocity, and handle its wall collision</summary>
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            Point direction = _inputService.GetDirection();
            Actor arrow = cast["arrows"][0];
            
            Player activePlayer = _rosterservice.GetActivePlayer(cast);

            if (activePlayer.isUser() && !activePlayer.HasShot())
            {
                Point arrowPosition = new Point(activePlayer.GetPosition().GetX() 
                    + (Constants.PLAYER_WIDTH  / 4),
                    activePlayer.GetPosition().GetY() - Constants.ARROW_GAP);
                
                arrow.SetPosition(arrowPosition);
                            
                if ((_inputService.IsLeftPressed() || _inputService.IsRightPressed()) && activePlayer.isUser() && activePlayer.HasMovement())
                {
                    
                    Point velocity = new Point
                    (
                        direction.Scale(Constants.PLAYER_SPEED).GetX(),
                        3
                    );
                    activePlayer.SetVelocity(velocity);
                    activePlayer.LoseMovement();

                }
                else 
                {
                    StopActor(activePlayer);
                }    
            }
        }

        ///<summary>Return actor x velocity to 0 so that it does not move continuously</summary>
        ///<param name="actor actor">
        private void StopActor(Actor actor)
        {
            int y = actor.GetVelocity().GetY();
            Point velocity = new Point(0,y);
            actor.SetVelocity(velocity);
        }

        
    }
}