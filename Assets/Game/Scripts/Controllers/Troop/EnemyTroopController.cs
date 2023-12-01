using Game.Scripts.Behaviours.UI;
using UnityEngine;

namespace Game.Scripts
{
    public class EnemyTroopController: TroopControllerBase
    {
        public TroopStateBehaviour TroopStateBehaviour;
        public override void StartAttack(TroopController troopController)
        {
         
        }
    }

    public class TroopStateBehaviour : MonoBehaviour
    {
    }
}