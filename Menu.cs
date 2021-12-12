using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Casting;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject
{
    public class Menu
    {
        private Dictionary<string, List<Actor>> _cast;
        private Dictionary<string, List<Action>> _script;
        private bool _keepMenuOpen = true;


        public Menu(Dictionary<string, List<Actor>> cast, Dictionary<string, List<Action>> script)
        {
            _cast = cast;
            _script = script;
        }

        /// <summary>
        /// This method starts the game and continues running until it is finished.
        /// </summary>
        public void OpenMenu()
        {
            while (_keepMenuOpen)
            {
                CueAction("input");
                // CueAction("update");
                CueAction("output");


                
                if (Raylib_cs.Raylib.GetKeyPressed() == 32 && !(HasUser(_cast) == HasAI(_cast)))
                {
                    _keepMenuOpen = false;
                }

                if (Raylib_cs.Raylib.WindowShouldClose())
                {
                    _keepMenuOpen = false;
                }
            }
        }

        /// <summary>
        /// Executes all of the actions for the provided phase.
        /// </summary>
        /// <param name="phase"></param>
        private void CueAction(string phase)
        {
            List<Action> actions = _script[phase];

            foreach (Action action in actions)
            {
                action.Execute(_cast);
            }
        }

        public bool HasUser(Dictionary<string, List<Actor>> cast)
        {
            if (int.Parse(cast["inputboxes"][1].GetText()) > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public bool HasAI(Dictionary<string, List<Actor>> cast)
        {
            if(int.Parse(cast["inputboxes"][2].GetText()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}