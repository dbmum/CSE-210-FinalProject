using System;
using System.Collections.Generic;

namespace CSE_210_FinalProject
{
    public class Menu
    {
        private bool _keepMenuOpen = true;

        public Menu(Dictionary<string, List<Actor>> cast, Dictionary<string, List<Action>> script)
        {
            _cast = cast;
            _script = script;
        }
        
        
    }
}