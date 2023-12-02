using UnityEngine;

namespace Game.Scripts.Data
{
    
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/AttackData", order = 0)]
    public class AttackData : ScriptableObject
    {
        [Header("Display Info")] public string attackName;
        public Vector2Int AttackPowerBaseRange;
        public AttackType AttackType;
        public float cooldownTime;

        public float GetRandomValueInRange()
        {
            return Random.Range(AttackPowerBaseRange.x, AttackPowerBaseRange.y);
        }
    }
}