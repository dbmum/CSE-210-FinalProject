using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class MoveActorsAction : Action
    {
        public MoveActorsAction()
        {
            
        }
        /// <summary>Loop through and move each individual actor it's velocity</summary>
        /// <param name="cast">
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            foreach (List<Actor> group in cast.Values)
            {
                foreach (Actor actor in group)
                {
                    MoveActor(actor);
                }
            }
        }

        /// <summary>Move an actor its velocity by scaling 
        ///the old position to the velocity</summary>
        ///<param name="actor actor">
        private void MoveActor(Actor actor)
        {
            int x = actor.GetX();
            int y = actor.GetY();

            int dx = actor.GetVelocity().GetX();
            int dy = actor.GetVelocity().GetY();

            // screenwrapping
            // int newX = (x + dx) % Constants.MAX_X;
            // int newY = (y + dy) % Constants.MAX_Y;
            
            // no screenwrapping
            int newX = (x + dx);
            int newY = (y + dy);


            actor.SetPosition(new Point(newX, newY));
        }
    }
}