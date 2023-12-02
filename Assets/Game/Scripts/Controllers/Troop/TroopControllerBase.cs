using System.Collections.Generic;
using Game.Scripts.Behaviours.Troop;
using Game.Scripts.Behaviours.Troop.Attack;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Controllers.Troop
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

        public delegate void UnitDiedEventHandler(TroopControllerBase unit);

        public event UnitDiedEventHandler TroopDiedEvent;
        public AttackSignals attackSignals;
        [Header("Debug")] public bool initializeOnStart;

        private void Start()
        {
            if (initializeOnStart)
            {
                SetHealth();
            }
        }

        public void Hit(bool isPlayer = true)
        {
            var targets = targetsBehaviour.FilterTargetUnits(AttackType.Solo);
            attackBehaviour.AttackAction(targets, data.AttackPower);
        }

        public void SetTroopData()
        {
            SetHealth();
        }

        #region TroopHealth

        private void SetHealth()
        {
            healthBehaviour.SetupCurrentHealth(data.health);
        } 

        public void RecieveTargetValue(float damage)
        {
            if (healthBehaviour.unitIsAlive)
            {
                healthBehaviour.ChangeHealth(damage);
                if(!healthBehaviour.unitIsAlive)
                    TroopDiedEvent?.Invoke(this);
            }
        }

        #endregion


        #region TargetTroops

        public void AssignTargetTroops(List<TroopControllerBase> aliveTroop) =>
            targetsBehaviour.AddTargetUnits(aliveTroop);

        public void RemoveTargetTroop(TroopControllerBase unit) => targetsBehaviour.RemoveTargetUnit(unit);

        #endregion

        protected virtual void OnUnitDiedEvent(TroopControllerBase unit)
        {
          
        }
    }
}