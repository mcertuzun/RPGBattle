using DG.Tweening;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using Game.Scripts.Utilities.Pooling;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.Attack
{
    public class BasicAttack : MonoBehaviour
    {
        public AttackSignals attackSignals;

        public void To(TroopControllerBase target, float damage)
        {
            transform.DOJump(target.transform.position, 1f, 1, 1f)
                .OnComplete(() =>
                {
                    target.RecieveTargetValue(damage);
                    attackSignals.TriggerEnd(target.data.teamType == TeamType.Enemy);
                });
        }
    }
}