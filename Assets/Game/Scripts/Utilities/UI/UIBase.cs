using System;
using Game.Scripts.Utilities.StateSystem;
using UnityEngine;

namespace Game.Scripts.Utilities.UI
{
    public abstract class CanvasBase : MonoBehaviour, IStateListener, IActivator
    {
        public StateMask stateMask;
        [SerializeField] private StateManager StateManager;
        
        public void StartListen()
        {
            StateManager.OnStateChange += OnStateChange;
            OnStateChange(StateManager.GetState());
        }

        public void StopListen()
        {
            StateManager.OnStateChange -= OnStateChange;
        }

        public void OnStateChange(UIState uiState)
        {
            SetActivate(stateMask.GamesStateContains(uiState));
        }

        public void SetActivate(bool val)
        {
            gameObject.SetActive(val);
        }

        protected virtual void OnDestroy()
        {
            StopListen();
        }
    }

    public enum UIState
    {
        InGameUI,
        VictoryUI,
        DefeatUI,
        SelectTroopsUI
    }
}