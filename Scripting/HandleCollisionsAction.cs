using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class HandleCollisionsAction : Action
    {
        PhysicsService _physicsService;
        RosterService _rosterService;
        public HandleCollisionsAction(PhysicsService physicsService, RosterService rosterService)
        {
            _physicsService = physicsService;
            _rosterService = rosterService;
        }
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            List<Actor> terrainList = cast["terrain"];
            List<Actor> bulletsToRemove = new List<Actor>();
            List<Actor> playersToRemove = new List<Actor>();
            foreach (Player player in cast["players"])
            {
                PlayerCollidesWithTerrain(player, terrainList);
            }

            foreach (Bullet bullet in cast["projectiles"])
            {
                bool hasImpacted = false;
                foreach (Player player in cast["players"])
                {
                    if (!player.GetStatus())
                    {
                        if (_physicsService.IsCollision(player, bullet))
                        {
                            player.LoseLife(Constants.BULLET_DAMAGE);
                            bulletsToRemove.Add(bullet);
                            hasImpacted = true;

                            if (player.GetLife() <= 0)
                            {
                                playersToRemove.Add(player);
                                break;
                            }
                            
                        }
                    }
                }
                
                if (!hasImpacted)
                {
                    foreach (Terrain terrain in terrainList)
                    {
                        if (_physicsService.IsCollision(bullet, terrain))
                        {
                            bulletsToRemove.Add(bullet);
                            int terrainX = terrain.GetPosition().GetX();
                            ImpactTerrain(terrainX, terrainList);
                            break;
                        }
                    }
                }
                
            }
            foreach (Actor player in playersToRemove)
            {
                cast["players"].Remove(player);
            }
            foreach (Actor bullet in bulletsToRemove)
            {
                cast["projectiles"].Remove(bullet);
                _rosterService.NextTurn(cast);
            }
            
        }
        private void ImpactTerrain(int terrainX, List<Actor> terrainList)
        {
            int tx = terrainList[terrainX].GetX();
            int ty = terrainList[terrainX].GetY();

            Point newPosition = new Point(tx,ty + Constants.IMPACT_AREA);

            terrainList[terrainX].SetPosition(newPosition);

            if (terrainX <= Constants.MAX_X - Constants.IMPACT_AREA - 1)
            {
                // right side impact
                for (int i = 0; i <= Constants.IMPACT_AREA; i++ )
                {
                    int side = terrainX + i + 1;
                    int x = terrainList[side].GetX();
                    int y = terrainList[side].GetY();

                    Point newPositionSide = new Point(x,y + (Constants.IMPACT_AREA - i));

                    terrainList[side].SetPosition(newPositionSide);
                }
            }

            if(terrainX >= Constants.IMPACT_AREA + 1)    
                // left side impact
                for (int i = -Constants.IMPACT_AREA; i <= -1; i++ )
                {
                    int side = terrainX + i;
                    int x = terrainList[side].GetX();
                    int y = terrainList[side].GetY();

                    Point newPositionSide = new Point(x,y + (Constants.IMPACT_AREA + i));

                    terrainList[side].SetPosition(newPositionSide);
                }
        }

        private void PlayerCollidesWithTerrain(Actor actor, List<Actor> terrainList)
        {
            foreach (Terrain terrain in terrainList)
            {
                if (_physicsService.IsCollision(actor, terrain))
                {

                    Point newposition = new Point
                    (
                        actor.GetPosition().GetX(),
                        terrain.GetPosition().GetY() - Constants.PLAYER_HEIGHT + 1
                    );

                    Point newVelocity = new Point
                    (
                        actor.GetVelocity().GetX(), 0
                    );

                    actor.SetPosition(newposition);
                    actor.SetVelocity(newVelocity);
                    break;
                }
            }
        }
    }
}