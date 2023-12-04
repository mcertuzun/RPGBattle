using System;
using System.Collections.Generic;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopTargetBehaviour : MonoBehaviour
    {
        [Header("Targets")] public List<TroopControllerBase> targetTroops;

        public void AddTargetUnits(List<TroopControllerBase> addedUnits)
        {
            targetTroops.Clear();
            foreach (var troop in addedUnits)
            {
                targetTroops.Add(troop);
            }
        }

        public void RemoveTargetUnit(TroopControllerBase removedUnit)
        {
            if (targetTroops.Contains(removedUnit))
                targetTroops.Remove(removedUnit);
        }

        public List<TroopControllerBase> FilterTargetUnits(AttackType attackType)
        {
            List<TroopControllerBase> filteredUnits = new();

            if (targetTroops.Count <= 0)
            {
                return filteredUnits;
            }

            switch (attackType)
            {
                case AttackType.Solo:
                    int randomUnit = Random.Range(0, targetTroops.Count);
                    filteredUnits.Add(targetTroops[randomUnit]);
                    break;
                case AttackType.Area:
                    filteredUnits.AddRange(targetTroops);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(attackType), attackType, null);
            }

            return filteredUnits;
        }

        public void ResetTargets()
        {
            targetTroops.Clear();
        }

        public TroopControllerBase GetRandomTargetUnit()
        {
            int randomUnit = Random.Range(0, targetTroops.Count);
            return targetTroops[randomUnit];
        }

        public List<TroopControllerBase> GetAllTargetUnits()
        {
            return targetTroops;
        }
    }
}