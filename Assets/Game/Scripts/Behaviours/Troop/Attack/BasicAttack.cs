using Assets.Library.Pooling;
using DG.Tweening;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using Game.Scripts.Utilities.Pooling;
using Game.Scripts.Utilities.UI;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.Attack
{
    public class BasicAttack : MonoBehaviour
    {
        public AttackSignals attackSignals;
        [Header("Data")] public PoolInfo damageFly;
        public void To(TroopControllerBase target, float damage)
        {
            transform.DOJump(target.transform.position, 1f, 1, 1f)
                .OnComplete(() =>
                {
                    target.RecieveTargetValue(damage);
                    attackSignals.TriggerEnd(target.data.teamType == TeamType.Enemy);
                    PoolManager.Fetch(damageFly.PoolName, transform.position, true).GetComponent<FlyingText>().PlayFlyTween(damage.ToString());
                    GetComponent<PoolObject>().Release();
                });
        }
    }
}