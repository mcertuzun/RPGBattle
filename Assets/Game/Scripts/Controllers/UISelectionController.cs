using System;
using System.Collections.Generic;
using Game.Scripts.Behaviours.UI;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class UISelectionController : MonoBehaviour
    {
        [SerializeField] private List<UISelectionImageBehaviour> UITroops;
        public List<UISelectionImageBehaviour> selectedUITroops;

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
            if (UITroop.isSelected) selectedUITroops.Add(UITroop);
            else if (selectedUITroops.Contains(UITroop)) selectedUITroops.Remove(UITroop);
        }
    }
}