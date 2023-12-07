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
        
        public void SetTroopReady(Transform spawnTransform)
        {
            transform.position = spawnTransform.position;
            transform.SetParent(spawnTransform);
            
            healthBehaviour.SetupCurrentHealth(data.Health);
            levelBehaviour.SetupCurrentLevel(data.Level);
        }

        public void ResetTroop(Transform spawnTransform)
        {
            targetsBehaviour.ResetTargets();
            transform.position = spawnTransform.position;
            transform.SetParent(null);
        }
        
        public void Hit(bool isPlayer = true)
        {
            var targets = targetsBehaviour.FilterTargetUnits(AttackType.Solo);
            attackBehaviour.AttackAction(targets, data.AttackPower);
        }


        #region TroopHealth

  
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