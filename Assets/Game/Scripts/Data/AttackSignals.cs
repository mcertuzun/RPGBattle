using System;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "AttackSignal", menuName = "SO/AttackSignal", order = 0)]
    public class AttackSignals : ScriptableObject
    {
        public event Action AttackEndEvent;
        public event Action AttackStartEvent;
        private bool isNextAttackReady;


        public void TriggerStart()
        {
            if (!isNextAttackReady) return;
            isNextAttackReady = false;
            AttackStartEvent?.Invoke();
        }

        public void TriggerEnd()
        {
            isNextAttackReady = true;
            AttackEndEvent?.Invoke();
        }
    }
}