using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class Player : Actor
    {
        private bool _isUser;
        private int _team;
        private bool _isActivePlayer;
        private int _life;
        private bool _hasShot = false;
        public Player(int startingX, int startingY, bool isUser, int team)
        {
            Point staringPosition = new Point(startingX,startingY);
            Point velocity = new Point(0,0);
            SetWidth(Constants.PLAYER_WIDTH);
            SetHeight(Constants.PLAYER_HEIGHT);

            SetPosition(staringPosition);
            SetVelocity(velocity);

            _hasGravity = true;

            _life = Constants.LIFE_COUNT;

            _team = team;
            
            _isUser = isUser;

        }
        public bool GetStatus()
        {
            return _isActivePlayer;
        }
        public void SetActivity(bool isActive)
        {
            _isActivePlayer = isActive;
        }
        public bool isUser()
        {
            return _isUser;
        }
        public void LoseLife(int damage)
        {
            _life -= damage;
        }
        public int GetLife()
        {
            return _life;
        }
        public int GetTeam()
        {
            return _team;
        }
        public bool HasShot()
        {
            return _hasShot;
        }
        public void SetHasShot(bool hasShot)
        {
            _hasShot = hasShot;
        }
    }
}