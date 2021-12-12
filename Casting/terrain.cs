using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject.Casting
{
    public class Terrain : Actor
    {
        public Terrain(int startingX, int startingY)
        {
            Point staringPosition = new Point(startingX,startingY);
            Point velocity = new Point(0,0);
            SetWidth(Constants.TERRAIN_WIDTH);
            SetHeight(Constants.TERRAIN_HEIGHT);

            SetPosition(staringPosition);
            SetVelocity(velocity);
        }
    }
}