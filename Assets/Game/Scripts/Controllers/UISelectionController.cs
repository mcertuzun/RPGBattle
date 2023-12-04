using System;
using System.Collections.Generic;
using Game.Scripts.Behaviours.UI;
using Game.Scripts.Behaviours.UI.TroopSelection;
using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;
using UnityEngine.Events;

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
        public UnityEvent OnClickEvent;
        
        public void OnBattleCountChanged(int val)
        {
            if (val % 5 == 0)
            {
                troopSelectionCount++;
                SetCounter();
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

        public void OnClickedBattleFilter()
        {
            if (selectedUITroops.Count < troopSelectionCount)
            {
                textBehaviour.AlertText();
            }
            else
            {
                OnClickEvent?.Invoke();
            }
        }

        private void SetCounter()
        {
            textBehaviour.SetText($"({selectedUITroops.Count}/{troopSelectionCount})");
        }
        
        private void Awake()
        {
            StartListen();
        }

        private void OnDestroy()
        {
            StopListen();
        }

        private void StopListen()
        {
            for (var i = 0; i < UITroops.Count; i++)
            {
                UITroops[i].TroopSelectEvent -= OnUITroopSelection;
            }
        }
        private void StartListen()
        {
            for (var i = 0; i < UITroops.Count; i++)
            {
                UITroops[i].TroopSelectEvent += OnUITroopSelection;
            }
        }

    }
}