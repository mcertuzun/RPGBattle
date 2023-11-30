using System;
using Game.Scripts.Utilities.UI;
using UnityEngine;

namespace Game.Scripts.Utilities.StateSystem
{
    public class StateManager : MonoBehaviour
    {
        private static StateManager _stateManager;
        [SerializeField] private UIState currentState;
        public static event Action<UIState> OnStateChange;

        public static UIState GetState()
        {
            if (_stateManager == null)
            {
                _stateManager = new StateManager();
            }

            return _stateManager.currentState;
        }

        public static void SetState(UIState uiState)
        {
            if (_stateManager == null)
            {
                _stateManager = new StateManager();
            }

            _stateManager.currentState = uiState;
            OnStateChange?.Invoke(uiState);
        }
    }
}