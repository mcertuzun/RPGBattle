using System.Collections.Generic;
using Game.Scripts.Behaviours.UI;
using Game.Scripts.Behaviours.UI.TroopSelection;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class UISelectionController : MonoBehaviour
    {
        [SerializeField] private List<UISelectionImageBehaviour> UITroops;
        public List<UISelectionImageBehaviour> selectedUITroops;
        public UITextBehaviour textBehaviour;
        public int RequiredTroopCount = 3;

        private void Awake()
        {
            SetupUITroops();
        }

        public void SetupUITroops()
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
            if (selectedUITroops.Count <= RequiredTroopCount)
            {
                if (!UITroop.isSelected && selectedUITroops.Count != RequiredTroopCount)
                {
                    selectedUITroops.Add(UITroop);
                    UITroop.SwitchSelection();
                }
                else if (selectedUITroops.Contains(UITroop))
                {
                    selectedUITroops.Remove(UITroop);
                    UITroop.SwitchSelection();
                } else
                {
                    textBehaviour.AlertText();
                }
            }
           
        }

        private void SetCounter()
        {
            textBehaviour.SetText($"({selectedUITroops.Count}/{RequiredTroopCount})");
        }
    }
}