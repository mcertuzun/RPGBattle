using System;
using System.Collections.Generic;
using Game.Scripts.Utilities.UI;
using UnityEngine;

namespace Game.Scripts.Utilities.StateSystem
{
    public class UIStateController : MonoBehaviour
    {
        [SerializeField] private List<UIBase> UIList;
        [SerializeField] private UIState currentState;
        public event Action<UIState> OnStateChange;

        public void StartListen()
        {
            for (var i = 0; i < UIList.Count; i++)
            {
                OnStateChange += UIList[i].OnUiStateChange;
            }
        }

        public void StopListen()
        {
            for (var i = 0; i < UIList.Count; i++)
            {
                OnStateChange -= UIList[i].OnUiStateChange;
            }
        }

        public UIState GetState()
        {
            return currentState;
        }

        public void SetState(UIState uiState)
        {
            currentState = uiState;
            OnStateChange?.Invoke(uiState);
        }
    }
}