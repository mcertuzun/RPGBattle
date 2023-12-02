using System.Collections.Generic;
using Game.Scripts.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{

   
    public class UITroopSelectionBehaviour : MonoBehaviour
    {
        public List<TroopData> Troops;
        public List<UISelectionImageBehaviour> troopsUI;
        public GameObject selectionImagePrefab;
        public Transform grid;
        
        public void CreateTroopsSelectionUI()
        {
            for (var i = 0; i < Troops.Count; i++)
            {
                // var newUI = Instantiate(selectionImagePrefab, grid).GetComponent<UISelectionImageBehaviour>();
                // newUI.SetUI(Troops[i].Name,Troops[i].Level,Troops[i].AttackPower,Troops[i].Experience);
            }
        }
        
    }
}