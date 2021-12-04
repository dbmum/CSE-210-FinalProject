using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class RosterService
    {
        public RosterService()
        {
            
        }

        public Player GetActivePlayer(Dictionary<string, List<Actor>> cast)
        {   
            Player activePlayer = null;
            foreach (Player player in cast["players"])
            {
                
                bool isActive = player.GetStatus();                
                if (isActive)
                {
                    activePlayer = player;
                    break;
                }

            }

            return activePlayer;
        }

        public void NextTurn(Dictionary<string, List<Actor>> cast)
        {
            List<Actor> players = cast["players"];
            Player  currentActive = GetActivePlayer(cast);
            int currentTeam = currentActive.GetTeam();
            Player newActive = new Player(-100,-100, false, 0);
            int newTeam = 0;
            
            // set current to the new past player and update index of last player on opposite team
            if (currentTeam == 1)
            {
                newTeam = 2;
            }
            else if (currentTeam == 2)
            {
                newTeam = 1;
            }
            currentActive.SetActivity(false);

            HandleTeamShot(newTeam, cast);

            foreach (Player player in players)
            {
                if(player.GetTeam() == newTeam)
                {
                    if (!player.HasShot())
                    {
                        newActive = player;
                        break;
                    }
                    
                }
            }

            newActive.SetActivity(true);
        }

        public int TeamCount(int teamNumber, Dictionary<string, List<Actor>> cast)
        {
            List<Actor> players = cast["players"];
            int teamCount = 0;
            foreach (Player player in players)
            {
                int team = player.GetTeam();
                if (team == teamNumber)
                {
                    teamCount++;
                }
            }
            return teamCount;
        }

        public void HandleTeamShot(int teamNumber, Dictionary<string, List<Actor>> cast)
        {
            List<Actor> players = cast["players"];
            int hasShotCount = 0;
            foreach (Player player in players)
            {
                int team = player.GetTeam();
                if (team == teamNumber)
                    {
                        if (player.HasShot())
                        {
                            hasShotCount++;
                        }
                        
                    }
            }

            if (hasShotCount == TeamCount(teamNumber, cast))
            {
                foreach (Player player in players)
                    {
                        int team = player.GetTeam();
                        if (team == teamNumber)
                        {
                            player.SetHasShot(false);
                        }
                    }
            }
        }



    }
}