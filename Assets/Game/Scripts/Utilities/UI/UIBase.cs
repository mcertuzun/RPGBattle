using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Utilities.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        public StateMask stateMask;
      
        public void OnUiStateChange(UIState uiState)
        {
            SetActivate(stateMask.GamesStateContains(uiState));
        }

        public void SetActivate(bool val)
        {
            gameObject.SetActive(val);
        }

    }

    public enum UIState
    {
        InGameUI=0,
        VictoryUI,
        DefeatUI,
        SelectTroopsUI
    }
}