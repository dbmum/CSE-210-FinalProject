using System;
using System.Collections.Generic;
using CSE_210_FinalProject.Services;
using CSE_210_FinalProject.Casting;
using CSE_210_FinalProject.Scripting;

namespace CSE_210_FinalProject
{
    /// <summary>
    /// The base class of all other actions.
    /// </summary>
    public abstract class Action
    {
        public abstract void Execute(Dictionary<string, List<Actor>> cast);
    }
}