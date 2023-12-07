using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

public class CheatCode : MonoBehaviour
{
    public TroopDataList allAllies;
    public UnityEvent<int> AddBattleCount; 
    public UnityEvent ResetBattleCount; 

    public void AddExperience(int val)
    {
        foreach (var troopData in allAllies.Value)
        {
            troopData.GainExperience(val);
            troopData.Save();
        }
    }
    public void AddLevel()
    {
        foreach (var troopData in allAllies.Value)
        {
            if (troopData.Experience<5)
            {
                troopData.Experience = 5;
            }
            troopData.LevelUp();
            troopData.Save();
        }
    }

    public void AddAttackPower(int val)
    {
        foreach (var troopData in allAllies.Value)
        {
            troopData.AttackPower+=val;
            troopData.Save();
        }
    }

    public void AddBattleRound(int val)
    {
        var current = PlayerPrefs.GetInt("troopSelectionCount");
        PlayerPrefs.SetInt("troopSelectionCount", ++current);
        AddBattleCount?.Invoke(current);
    }

    public void ResetData()
    {
        for (var i = 0; i < allAllies.Value.Count; i++)
        {
            allAllies.Value[i].Health = 100;
            allAllies.Value[i].AttackPower = 10;
            allAllies.Value[i].Level = 0;
            allAllies.Value[i].Experience = 0;
            allAllies.Value[i].Save();
        }
        PlayerPrefs.SetInt("troopSelectionCount", 2);
        ResetBattleCount?.Invoke();

    }
}
