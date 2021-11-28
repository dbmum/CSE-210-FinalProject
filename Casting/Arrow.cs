using System;
using System.Collections.Generic;
namespace CSE_210_FinalProject
{
    public class Arrow : Actor
    {
        public Arrow()
        {
            Point staringPosition = new Point(0,0);
            Point velocity = new Point(0,0);
            SetWidth(Constants.ARROW_WIDTH);
            SetHeight(Constants.ARROW_HEIGHT);

            SetPosition(staringPosition);
            SetVelocity(velocity);
        }
    }
}