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

        [Header("Level Settings")] [SerializeField]
        protected TroopLevelBehaviour levelBehaviour;

        public delegate void UnitDiedEventHandler(TroopControllerBase unit);

        public event UnitDiedEventHandler TroopDiedEvent;
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
        
        public void ResetTroop()
        {
            SetHealth();
            targetsBehaviour.ResetTargets();
        }
        

        #region TroopHealth

        public void SetHealth()
        {
            healthBehaviour.SetupCurrentHealth(data.Health);
        }

        public void GetHealth()
        {
            healthBehaviour.GetCurrentHealth();
        }

        public void RecieveTargetValue(float damage)
        {
            if (healthBehaviour.unitIsAlive)
            {
                healthBehaviour.ChangeHealth(damage);
                if (!healthBehaviour.unitIsAlive)
                    TroopDiedEvent?.Invoke(this);
            }
        }

        #endregion


        #region TargetTroops

        public void AssignTargetTroops(List<TroopControllerBase> aliveTroop) =>
            targetsBehaviour.AddTargetUnits(aliveTroop);

        public void RemoveTargetTroop(TroopControllerBase unit) => targetsBehaviour.RemoveTargetUnit(unit);

        #endregion
    }
}