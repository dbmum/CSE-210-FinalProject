using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class Player : Actor
    {
        bool _isActivePlayer;
        bool _isUser;
        public Player(int startingX, int startingY, bool isActivePlayer, bool isUser)
        {
            Point staringPosition = new Point(startingX,startingY);
            Point velocity = new Point(0,0);
            SetWidth(Constants.PLAYER_WIDTH);
            SetHeight(Constants.PLAYER_HEIGHT);

            SetPosition(staringPosition);
            SetVelocity(velocity);

            if (isUser)
            {
                _isUser = true;
            }
            else 
            {
                _isUser = false;
            }

            if (isActivePlayer)
            {
                _isActivePlayer = true;
            }
            else
            {
                _isActivePlayer = false;
            }
        }
        public bool GetStatus()
        {
            return _isActivePlayer;
        }
        public bool isUser()
        {
            return _isUser;
        }
    }
}