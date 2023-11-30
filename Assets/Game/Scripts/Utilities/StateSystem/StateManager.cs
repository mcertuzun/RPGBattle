using System;
using UnityEngine;

namespace Game.Scripts.Utilities.UI
{
    public class StateManager 
    {
        private static StateManager _stateManager;
        [SerializeField] private GameState currentState;
        public static event Action<GameState> OnStateChange;

        public static GameState GetState()
        {
            if (_stateManager == null)
            {
                _stateManager = new StateManager();
            }

            return _stateManager.currentState;
        }

        public static void SetState(GameState gameState)
        {
            if (_stateManager == null)
            {
                _stateManager = new StateManager();
            }

            _stateManager.currentState = gameState;
            OnStateChange?.Invoke(gameState);
        }
    }
}