using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject.Casting
{
    public class Player : Actor
    {
        private int _AIdifficulty;
        private bool _isUser;
        private int _team;
        private bool _isActivePlayer;
        private int _life;
        private bool _hasShot = false;
        private int _movementRemaining = Constants.MAX_MOVEMENT;
        private bool _AIcanShoot;

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

            if (!_isUser)
            {
                _AIcanShoot = false;
            }

        }
        public void SetAIdifficulty(int difficulty)
        {
            _AIdifficulty = difficulty;
        }
        public int GetDIfficulty()
        {
            return _AIdifficulty;
        }
        public void SetCanShoot(bool canShoot)
        {
            _AIcanShoot = canShoot;
        }
        public void LoseMovement()
        {
            _movementRemaining -= 1;
        }
        public void ResetMovement()
        {
            _movementRemaining = Constants.MAX_MOVEMENT;
        }
        public bool HasMovement()
        {
            return (_movementRemaining > 0);
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