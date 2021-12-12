using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject.Casting
{
    public class Arrow : Actor
    {
        public Arrow()
        {
            Point staringPosition = new Point(0,0);
            Point velocity = new Point(0,0);
            SetWidth(Constants.ARROW_WIDTH);
            SetHeight(Constants.ARROW_HEIGHT);

            SetImage(Constants.ARROW_IMAGE);

            SetPosition(staringPosition);
            SetVelocity(velocity);
        }
    }
}