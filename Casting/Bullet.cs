using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject.Casting
{
    public class Bullet : Actor
    {
        public Bullet(int startingX, int startingY, Point velocity)
        {
            Point staringPosition = new Point(startingX,startingY);
            SetWidth(Constants.BULLET_WIDTH);
            SetHeight(Constants.BULLET_HEIGHT);

            SetImage(Constants.BULLET_IMAGE);

            SetPosition(staringPosition);
            SetVelocity(velocity);

            _hasGravity = true;
        }
    }
}