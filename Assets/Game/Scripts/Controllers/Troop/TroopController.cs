using Game.Scripts.Behaviours;
using Game.Scripts.Behaviours.UI;
using UnityEngine;

namespace Game.Scripts
{
    public class TroopController : TroopControllerBase
    {
        [Header("Attack Event")] [SerializeField]
        public SpriteClickBehaviour SpriteClickBehaviour;

        private void OnEnable()
        {
            SpriteClickBehaviour.StartAttackEvent.AddListener(StartAttack);
        }

        public override void StartAttack(TroopController troopController)
        {
            var targets = targetsBehaviour.FilterTargetUnits(AttackType.Solo);
            attackBehaviour.AttackAction(targets, this);
        }
    }
}