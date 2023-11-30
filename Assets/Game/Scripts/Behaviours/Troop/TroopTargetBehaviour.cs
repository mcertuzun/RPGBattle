using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Behaviours
{
    public enum TargetType
    {
        Random,
        Order
    }

    public class TroopTargetBehaviour : MonoBehaviour
    {
        [Header("Targets")] public List<TroopController> targetTroops;

        public void AddTargetUnits(List<TroopController> addedUnits)
        {
            targetTroops.Clear();
            foreach (var troop in addedUnits)
            {
                targetTroops.Add(troop);
            }
        }

        public void RemoveTargetUnit(TroopController removedUnit)
        {
            targetTroops.Remove(removedUnit);
        }

        public List<TroopController> FilterTargetUnits(TargetType targetType)
        {
            List<TroopController> filteredUnits = new List<TroopController>();

            if (targetTroops.Count <= 0)
            {
                return filteredUnits;
            }

            switch (targetType)
            {
                case TargetType.Random:
                    int randomUnit = Random.Range(0, targetTroops.Count);
                    filteredUnits.Add(targetTroops[randomUnit]);
                    break;
                case TargetType.Order:
                    //Maybe later.. Check it according to the case count
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }

            return filteredUnits;
        }

        public TroopController GetRandomTargetUnit()
        {
            int randomUnit = Random.Range(0, targetTroops.Count);
            return targetTroops[randomUnit];
        }

        public List<TroopController> GetAllTargetUnits()
        {
            return targetTroops;
        }
    }
}