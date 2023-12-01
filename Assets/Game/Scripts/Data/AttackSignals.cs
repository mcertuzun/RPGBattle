using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.Attack
{
    public class AttackSignals : ScriptableObject
    {
        public delegate void OnAttackEnd();
        public event OnAttackEnd AttackEndEvent;
        
        public delegate void OnAttackStart();
        public event OnAttackStart AttackStartEvent;
        
        public void TriggerStart()
        {
            AttackEndEvent?.Invoke();
        }
        public void TriggerEnd()
        {
            AttackEndEvent?.Invoke();
        }
    }
}