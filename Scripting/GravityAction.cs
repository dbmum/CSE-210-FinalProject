using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class GravityAction : Action
    {
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            foreach (List<Actor> group in cast.Values)
            {
                foreach (Actor actor in group)
                {
                    if (actor.HasGravity())
                    {
                        ApplyGravity(actor);
                    }
                }
            }
        }

        private void ApplyGravity(Actor actor)
        {
            Point velocity = actor.GetVelocity();
            Point newVelocity = new Point(velocity.GetX(), velocity.GetY() + Constants.GRAVITY_RATE);

            if (newVelocity.GetY() > Constants.GRAVITY_CAP)
            {
                Point capVelocity = new Point(newVelocity.GetX(), Constants.GRAVITY_CAP);
                newVelocity = capVelocity;
            }

            actor.SetVelocity(newVelocity);
        }
    }
}