using Game.Scripts.Behaviours;
using Game.Scripts.Behaviours.Troop;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class TroopControllerBase : MonoBehaviour
    {
    }

    public class TroopController : TroopControllerBase
    {
        [Header("Data")] public TroopData data;

        [Header("Health Settings")] public TroopHealthBehaviour healthBehaviour;

        [Header("Target Settings")] public TroopTargetBehaviour targetsBehaviour;

        [Header("Attack Settings")] public TroopAttacksBehavior attacksBehaviour;

        public delegate void UnitDiedEventHandler(TroopController unit);

        public event UnitDiedEventHandler UnitDiedEvent;

        public void SetTroopData()
        {
        }

        public void SetHealth() => healthBehaviour.SetupCurrentHealth(data.health);
        public void RemoveTargetUnit(TroopController unit) => targetsBehaviour.RemoveTargetUnit(unit);
    }
}