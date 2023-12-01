using Game.Scripts.Behaviours;
using UnityEngine;

namespace Game.Scripts.Controllers.Troop
{
    public class TroopController : TroopControllerBase
    {
        [Header("Attack Event")] [SerializeField]
        public SpriteClickBehaviour SpriteClickBehaviour;
        private void OnEnable()
        {
            SpriteClickBehaviour.StartAttackEvent += StartBattle;
        }
    }
}