using System.Collections.Generic;
using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopAttacksBehavior : MonoBehaviour
    {
        public TroopAttackBehaviour[] attacks;
        [SerializeField]
        private Queue<int> attackQueue;

        [SerializeField]
        [ReadOnly]private bool attackCastAllow;

        [SerializeField]
        [ReadOnly]private bool attackCurrentlyActive;
        
        void Awake()
        {
            attackQueue = new Queue<int>();
            SetupAttacks();
        }
        
        void SetupAttacks()
        {
            for(int i = 0; i < attacks.Length; i++)
            {
                attacks[i].SetupID(i);
                attacks[i].SetupAttackCooldownTimer();;
            }
        }
        
        public void StartAttackCooldowns()
        {
            attackCastAllow = true;
            attackCurrentlyActive = false;

            for(int i = 0; i < attacks.Length; i++)
            {
                attacks[i].StartAttackCooldown();
            }
        }

        public void StopAllAttacks()
        {
            throw new System.NotImplementedException();
        }
    }
}