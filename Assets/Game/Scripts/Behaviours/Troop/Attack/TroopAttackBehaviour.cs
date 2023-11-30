using Game.Scripts.Data;
using Game.Scripts.Timer;
using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Behaviours.Troop
{
    public class AttackBehaviourBase : MonoBehaviour
    {
    }

    public class TroopAttackBehaviour : AttackBehaviourBase
    {
        [Header("Data")] public AttackData data;

        [Header("Runtime ID")] [SerializeField] [ReadOnly]
        private int ID;

        [Header("Timer")] private DurationTimer cooldownTimer;
        [ReadOnly] [SerializeField] public bool cooldownActive;
        [ReadOnly] [SerializeField] public bool attackReady;

        public delegate void AttackReadyEventHandler();
        public event AttackReadyEventHandler AttackReadyEvent;

        [Header("Events")] public UnityEvent<int> attackReadyForQueue;
        public UnityEvent<int, TargetType> applyAttackValueToTargets;
        public UnityEvent<int> attackSequenceFinished;

        public void SetupAttackCooldownTimer()
        {
            cooldownTimer = new DurationTimer(data.cooldownTime);
        }

        public void StartAttackCooldown()
        {
            cooldownActive = true;
            attackReady = false;
        }

        void Update()
        {
            CheckAttackCooldown();
        }

        void CheckAttackCooldown()
        {
            if (cooldownActive)
            {
                cooldownTimer.UpdateTimer();
                if (cooldownTimer.HasElapsed())
                {
                    cooldownTimer.EndTimer();
                    cooldownTimer.Reset();
                    AbilityCooldownFinished();
                    return;
                }
            }
        }

        void AbilityCooldownFinished()
        {
            cooldownActive = false;
            attackReady = true;
            AttackReadyEvent?.Invoke();
        }

        public void SetupID(int id)
        {
            ID = id;
        }
    }
}