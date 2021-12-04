using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class Bullet : Actor
    {
        public Bullet(int startingX, int startingY, Point velocity)
        {
            Point staringPosition = new Point(startingX,startingY);
            SetWidth(Constants.BULLET_WIDTH);
            SetHeight(Constants.BULLET_HEIGHT);

            SetPosition(staringPosition);
            SetVelocity(velocity);

            _hasGravity = true;
        }
    }
}