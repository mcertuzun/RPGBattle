using Game.Scripts.Controllers;
using Game.Scripts.Data;
using Game.Scripts.Utilities.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UISelectionController uiSelectionController;
        [SerializeField] private UIStateController uiStateController;
        [SerializeField] private TroopDataList allyTroops;
        public UnityEvent OnNextBattleEvent;

        public void OnTroopSelection()
        {
            uiStateController.SetState((int)UIState.SelectTroopsUI);
        }

        public void OnVictory()
        {
            uiStateController.SetState((int)UIState.VictoryUI);
        }

        public void OnDefeat()
        {
            uiStateController.SetState((int)UIState.DefeatUI);
        }

        public void OnNextBattle()
        {
            SetAllyData();
            uiStateController.SetState((int)UIState.InGameUI);
        }

        public void OnCheat()
        {
            uiStateController.SetState((int)UIState.CheatUI);
        }
        private void SetAllyData()
        {
            allyTroops.Value.Clear();
            for (var i = 0; i < uiSelectionController.selectedUITroops.Count; i++)
            {
                allyTroops.Value.Add(uiSelectionController.selectedUITroops[i].troopData);
            }
        }
    }
}