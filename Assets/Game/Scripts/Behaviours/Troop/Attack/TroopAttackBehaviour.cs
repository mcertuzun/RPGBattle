using System.Collections.Generic;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using Game.Scripts.Utilities.Pooling;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.Attack
{
    public class AttackBehaviourBase : MonoBehaviour
    {
    }

    public class TroopAttackBehaviour : AttackBehaviourBase
    {
        [Header("Data")] 
        public PoolInfo poolInfo;

        public void AttackAction(List<TroopControllerBase> targetTroops)
        {
            
            var missile = PoolManager.Fetch(poolInfo.PoolName, transform.position, true).GetComponent<BasicAttack>();
            foreach (var target in targetTroops)
            {
                missile.To(target);
            }
        }
    }
}