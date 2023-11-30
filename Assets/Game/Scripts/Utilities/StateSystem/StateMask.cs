using System;
using System.Collections.Generic;

namespace Game.Scripts.Utilities.UI
{
    [Serializable]
    public class StateMask
    {
        public List<GameState> gameStates = new();

        public bool GamesStateContains(GameState gameState)
        {
            return gameStates.Contains(gameState);
        }
    }
}