using System.Collections.Generic;
using Game.Scripts.Behaviours;
using Game.Scripts.Behaviours.Troop;
using Game.Scripts.Behaviours.Troop.Attack;
using Game.Scripts.Data;
using UnityEngine;
using AttackType = Game.Scripts.Behaviours.AttackType;

namespace Game.Scripts
{
    public abstract class TroopControllerBase : MonoBehaviour
    {
        [Header("Data")] public TroopData data;

        [Header("Health Settings")] [SerializeField]
        public TroopHealthBehaviour healthBehaviour;

        [Header("Target Settings")] [SerializeField]
        protected TroopTargetBehaviour targetsBehaviour;

        [Header("Attack Settings")] [SerializeField]
        protected TroopAttackBehaviour attackBehaviour;
        public delegate void UnitDiedEventHandler(TroopController unit);
        public event UnitDiedEventHandler TroopDiedEvent;
        
        [Header("Debug")] public bool initializeOnStart;

        public abstract void StartAttack(TroopController troopController);
       
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
            //attacksBehaviour.StartAttackCooldowns();
        }
        
        public void BattleEnded()
        {
            //attacksBehaviour.StopAllAttacks();
        }
        
        private void SetHealth() => healthBehaviour.SetupCurrentHealth(data.health);

        public void SetTroopData()
        {
        }

        #region AttackTroops
        public void AttackTroop(int attackValue, AttackType attackType)
        {   
            List<TroopController> targetUnits = targetsBehaviour.FilterTargetUnits(attackType);
            if(targetUnits.Count > 0)
            {
                for(int i = 0; i < targetUnits.Count; i++)
                {
                    targetUnits[i].RecieveTargetValue(attackValue);
                }  
            } 
        }
        public void RecieveTargetValue(int abilityValue)
        {
            if(healthBehaviour.unitIsAlive)
            {
                healthBehaviour.ChangeHealth(abilityValue);
            }
        }

        #endregion

        #region TargetTroops
        public void AssignTargetTroops(List<TroopController> aliveTroop) => targetsBehaviour.AddTargetUnits(aliveTroop);
        public void RemoveTargetUnit(TroopController unit) => targetsBehaviour.RemoveTargetUnit(unit);
        #endregion

        protected virtual void OnUnitDiedEvent(TroopController unit)
        {
            TroopDiedEvent?.Invoke(unit);
        }
    }
}