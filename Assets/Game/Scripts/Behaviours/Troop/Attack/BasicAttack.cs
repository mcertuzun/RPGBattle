using DG.Tweening;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.Attack
{
    public class BasicAttack : MonoBehaviour
    {
        public AttackSignals attackSignals;
        public AttackData Data;

        public void To(TroopControllerBase target)
        {
            transform.DOJump(target.transform.position, 1f, 1, 1f)
                .OnComplete(() =>
                {
                    target.RecieveTargetValue(Data.GetRandomValueInRange());
                    attackSignals.TriggerEnd();
                });
        }
    }
}