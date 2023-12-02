using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class SpriteClickBehaviour : MonoBehaviour
    {
        public delegate void StartAttack(bool hit);

        public event StartAttack StartAttackEvent;

        public AttackSignals attackSignals;

        void OnMouseDown()
        {
            if (!attackSignals.canPlayerHit) return;
            StartAttackEvent?.Invoke(true);
            attackSignals.TriggerStart();
        }
    }
}