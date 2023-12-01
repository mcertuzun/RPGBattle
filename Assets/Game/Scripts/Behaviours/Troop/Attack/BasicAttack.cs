using DG.Tweening;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.Attack
{
    public class Attack01 : MonoBehaviour
    {
        public AttackSignals attackSignals;

        public void To(Transform transform, TroopController troopController)
        {
            transform.DOJump(transform.position, 1f, 1, 1f).OnComplete(() =>
            {
                attackSignals.TriggerEnd(troopController);
            });
        }
    }
}