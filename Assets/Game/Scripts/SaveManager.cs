using System.Collections.Generic;
using Game.Scripts.Data;
using Game.Scripts.Utilities.SaveSystem;
using UnityEngine;

namespace Game.Scripts
{
   
    public class SaveManager : MonoBehaviour
    {
        // [SerializeField] private TroopDataList troopDataList;
        // public int val=5;
        //
        // private void Awake()
        // {
        //   SaveSystem.LoadTroopsData(troopDataList.Value);
        // }
        //
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         foreach (var troopData in troopDataList.Value)
        //         {
        //             troopData.Experience += val;
        //         }
        //     }
        // }
        //
        // private void OnApplicationQuit()
        // {
        //     SaveSystem.SaveTroopsData(troopDataList.Value);
        // }
        //
        // private void OnDestroy()
        // {
        //     SaveSystem.SaveTroopsData(troopDataList.Value);
        // }
        //
        //
        // public struct TroopListWrap 
        // {
        //     public List<TroopData> troopList;
        // }
    }
}