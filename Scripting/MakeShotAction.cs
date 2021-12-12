using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Casting;

namespace CSE_210_FinalProject
{
    public class MakeShotAction : Action
    {
        RosterService _rosterService;
        InputService _inputService;
        PhysicsService _physicsService;
        AudioService _audioService;
        public MakeShotAction(RosterService rosterService, InputService inputService, PhysicsService physicsService, AudioService audioService)
        {
            _rosterService = rosterService;
            _inputService = inputService;
            _physicsService = physicsService;
            _audioService = audioService;
        }
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            Player activePlayer = _rosterService.GetActivePlayer(cast);

            // User Shot
            if (_inputService.IsMouseLeftClick() && !activePlayer.HasShot() && activePlayer.isUser())
            {
                int mouseX = _inputService.GetMouseX();
                int mouseY = _inputService.GetMouseY();

                int activePlayerX = activePlayer.GetPosition().GetX();
                int activePlayerY = activePlayer.GetPosition().GetY();

                int dx = mouseX - activePlayerX;
                int dy = mouseY - activePlayerY;

                double hyp =  Math.Sqrt((Math.Pow(dx,2)) + (Math.Pow(dy,2)));

                double velx = (((Constants.BULLET_SPEED + (hyp / Constants.POWER_SCALING) ) / hyp ) * dx);
                double vely =(((Constants.BULLET_SPEED + (hyp / Constants.POWER_SCALING)) / hyp ) * dy);
                
                


                Point bulletVelocity = new Point((int)velx, (int)vely);
                Bullet bullet = new Bullet(activePlayerX, activePlayerY - 10, bulletVelocity);
                cast["projectiles"].Add(bullet);

                activePlayer.SetHasShot(true);
                Point stop = new Point(0,0);
                activePlayer.SetVelocity(stop);
                if (activePlayer.GetTeam() == 1)
                {
                    _audioService.PlaySound(Constants.TEAM_1_GUN_SOUND);
                }
                else
                {
                    _audioService.PlaySound(Constants.TEAM_2_GUN_SOUND);
                }
            }

            // AI Shot
            else if (!activePlayer.HasShot() && !activePlayer.isUser())
            {
                Random rand = new Random();
                
                
                activePlayer.LoseMovement();
                // activePlayer.LoseMovement();
                
                if (!activePlayer.HasMovement())
                {
                    Actor closestEnemy = FindClosestEnemy(cast, activePlayer);
                    


                    // hit percentage a.k.a. difficulty
                    if (rand.Next(0,101) >= activePlayer.GetDIfficulty())
                    {
                    Actor ghostPlayer = new Actor();
                        ghostPlayer.SetWidth(Constants.PLAYER_WIDTH);
                        ghostPlayer.SetHeight(Constants.PLAYER_HEIGHT);
                        int offset = rand.Next(Constants.PLAYER_WIDTH + 5, Constants.PLAYER_WIDTH + 20);
                        if (rand.Next(0,2) == 1)
                        {
                            offset = -offset;
                        }
                        Point ghostPosition = new Point(closestEnemy.GetX() + offset, closestEnemy.GetY());
                        
                        ghostPlayer.SetPosition(ghostPosition);
                        closestEnemy = ghostPlayer;
                    }

                    Point shotVelocity = FindCorrectShot(activePlayer, closestEnemy);

                    Bullet bullet = new Bullet(activePlayer.GetX(), activePlayer.GetY(), shotVelocity);
                    cast["projectiles"].Add(bullet);

                    activePlayer.SetHasShot(true);
                    Point stop = new Point(0,0);
                    activePlayer.SetVelocity(stop);
                    _audioService.PlaySound(Constants.TEAM_2_GUN_SOUND);
                }
                else
                {

                }

                // // move AI
                
                // Point move = new Point(rand.Next(-30, 30), 0);
                // Point stopMove = new Point(0,0);
                // activePlayer.SetVelocity(move);
                // activePlayer.MoveNext();
            }


        }

        private Point FindCorrectShot(Actor shotStart, Actor desiredPoint)
        {
           
            bool hasMadeContact = false;
            int trueX = 0;
            int trueY = 0;

            for (int y = -50; y <= 50; y ++)
            {
                for (int x = -50; x <= 50; x ++)
                {
                    hasMadeContact = SimulateShot(x,y, shotStart, desiredPoint);
                    if (hasMadeContact)
                    {
                        trueX = x;
                        break;
                    }
                }

                if (hasMadeContact)
                {
                    trueY = y;
                    break;
                }
            }
            Point correctShotVelocity = new Point(trueX, trueY);
            return correctShotVelocity;
        }

        private bool SimulateShot(int shotVelocityX, int shotVelocityY, Actor shotStart, Actor desiredPoint)
        {
            Point testVelocity = new Point(shotVelocityX, shotVelocityY);
            Bullet testBullet = new Bullet(shotStart.GetX(), shotStart.GetY(), testVelocity);
            bool hasMadeContact = false;
            int testTime = 0;
            int testMax = 999999;

            while ((testBullet.GetX() < Constants.MAX_X && testBullet.GetX() > 0
                    && testBullet.GetY() < Constants.MAX_Y) || testTime > testMax)
            {
                if (_physicsService.IsCollision(testBullet, desiredPoint))
                {
                    hasMadeContact = true;
                    break;
                }
                else
                {
                    Point velocity = testBullet.GetVelocity();
                    Point newVelocity = new Point(velocity.GetX(), velocity.GetY() + Constants.GRAVITY_RATE);

                    if (newVelocity.GetY() > Constants.GRAVITY_CAP)
                    {
                        Point capVelocity = new Point(newVelocity.GetX(), Constants.GRAVITY_CAP);
                        newVelocity = capVelocity;
                    }

                    testBullet.SetVelocity(newVelocity);


                    testBullet.MoveNext();
                    
                }
                testTime++;
            }

            return hasMadeContact;
        }

        public Actor FindClosestEnemy(Dictionary<string, List<Actor>> cast, Player AI)
        {
            int maxDistance = 999999;
            Actor closestEnemy = new Actor() ;
            foreach (Player enemy in cast["players"])
            {
                if (AI.GetTeam() != enemy.GetTeam())
                {
                    int aiX = AI.GetPosition().GetX();
                    int aiY = AI.GetPosition().GetY();

                    int enemyX = enemy.GetPosition().GetX();
                    int enemyY = enemy.GetPosition().GetY();

                    int dx = enemyX - aiX;
                    int dy = enemyY - aiY;

                    double hyp =  Math.Sqrt((Math.Pow(dx,2)) + (Math.Pow(dy,2)));

                    if (hyp < maxDistance)
                    {
                        maxDistance = (int)hyp;
                        closestEnemy = enemy;
                    }
                }
            }
            return closestEnemy;
        }
    }
}