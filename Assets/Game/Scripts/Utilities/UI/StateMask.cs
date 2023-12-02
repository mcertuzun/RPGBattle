using System;
using System.Collections.Generic;

namespace Game.Scripts.Utilities.UI
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