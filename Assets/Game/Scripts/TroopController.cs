using System;
using System.Collections.Generic;
using Game.Scripts.Behaviours;
using Game.Scripts.Behaviours.Troop;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class TroopControllerBase : MonoBehaviour
    {
    }

    public class TroopController : TroopControllerBase
    {
        [Header("Data")] public TroopData data;

        [Header("Health Settings")] [SerializeField]
        public TroopHealthBehaviour healthBehaviour;

        [Header("Target Settings")] [SerializeField]
        private TroopTargetBehaviour targetsBehaviour;

        [Header("Attack Settings")] [SerializeField]
        private TroopAttacksBehavior attacksBehaviour;

        public delegate void UnitDiedEventHandler(TroopController unit);

        public event UnitDiedEventHandler UnitDiedEvent;
        [Header("Debug")] public bool initializeOnStart;

        private void Start()
        {
            if (initializeOnStart)
            {
                SetHealth();
                BattleStarted();
            }
        }

        public void BattleStarted()
        {
            attacksBehaviour.StartAttackCooldowns();
        }
        
        public void BattleEnded()
        {
            attacksBehaviour.StopAllAttacks();
        }
        
        private void SetHealth() => healthBehaviour.SetupCurrentHealth(data.health);

        public void SetTroopData()
        {
            throw new NotImplementedException();
        }

        #region TargetTroops
        public void AssignTargetTroops(List<TroopController> aliveTroop) => targetsBehaviour.AddTargetUnits(aliveTroop);
        public void RemoveTargetUnit(TroopController unit) => targetsBehaviour.RemoveTargetUnit(unit);
        #endregion

        protected virtual void OnUnitDiedEvent(TroopController unit)
        {
            UnitDiedEvent?.Invoke(unit);
        }
    }
}