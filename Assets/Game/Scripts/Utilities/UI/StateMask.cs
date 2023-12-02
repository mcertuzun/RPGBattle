using System;
using System.Collections.Generic;
using Game.Scripts.Utilities.UI;

namespace Game.Scripts.Utilities.StateSystem
{
    [Serializable]
    public class StateMask
    {
        public List<UIState> gameStates = new();

        public bool GamesStateContains(UIState uiState)
        {
            return gameStates.Contains(uiState);
        }
    }
}