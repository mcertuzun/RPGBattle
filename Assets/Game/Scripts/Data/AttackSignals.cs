using System;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "AttackSignal", menuName = "SO/AttackSignal", order = 0)]
    public class AttackSignals : ScriptableObject
    {
        public event Action AttackEndEvent;
        public event Action AttackStartEvent;
        public bool canPlayerHit = true;

        public void TriggerStart()
        {
            AttackStartEvent?.Invoke();
            canPlayerHit = false;
        }

        public void TriggerEnd(bool isAi)
        {
            AttackEndEvent?.Invoke();
            if(isAi)
                canPlayerHit = true;
        }

        private void OnDisable()
        {
            canPlayerHit = true;
        }
    }
}