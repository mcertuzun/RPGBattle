using System.Collections.Generic;
using Game.Scripts.Behaviours.UI;
using Game.Scripts.Behaviours.UI.TroopSelection;
using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class UISelectionController : MonoBehaviour
    {
        [SerializeField] private List<UISelectionImageBehaviour> UITroops;
        public List<UISelectionImageBehaviour> selectedUITroops;
        public UITextBehaviour textBehaviour;
        private const int DEFAULT_TROOP_SELECTION_COUNT = 3;
        public int troopSelectionCount
        {
            get
            {
                _troopSelectionCount = PlayerPrefs.GetInt(nameof(troopSelectionCount), DEFAULT_TROOP_SELECTION_COUNT);
                return _troopSelectionCount;
            }
            set
            {
                PlayerPrefs.SetInt(nameof(troopSelectionCount), value);
                _troopSelectionCount = value;
            }
        }

        [ReadOnly] [SerializeField] private int _troopSelectionCount;

        private void Awake()
        {
            SetupUITroops();
        }

        public void OnBattleCountChanged(int val)
        {
            if (val % 5 == 0)
            {
                troopSelectionCount++;
                SetCounter();
            }
        }

        private void SetupUITroops()
        {
            for (var i = 0; i < UITroops.Count; i++)
            {
                UITroops[i].TroopSelectEvent += OnUITroopSelection;
            }
        }

        private void OnUITroopSelection(UISelectionImageBehaviour UITroop)
        {
            UpdateSelectedTroops(UITroop);
            SetCounter();
        }

        private void UpdateSelectedTroops(UISelectionImageBehaviour UITroop)
        {
            if (selectedUITroops.Count <= troopSelectionCount)
            {
                if (!UITroop.isSelected && selectedUITroops.Count != troopSelectionCount)
                {
                    selectedUITroops.Add(UITroop);
                    UITroop.SwitchSelection();
                }
                else if (selectedUITroops.Contains(UITroop))
                {
                    selectedUITroops.Remove(UITroop);
                    UITroop.SwitchSelection();
                }
                else
                {
                    textBehaviour.AlertText();
                }
            }
        }

        private void SetCounter()
        {
            textBehaviour.SetText($"({selectedUITroops.Count}/{troopSelectionCount})");
        }
    }
}