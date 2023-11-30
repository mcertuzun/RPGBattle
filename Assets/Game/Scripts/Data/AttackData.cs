using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Data
{
    public enum AttackType
    {
        Ranged,
        Melee
    }
    public enum TargetType
    {
        Random,
        Ordered
    }
    
    public class Stats : ScriptableObject
    {
        public string Name;
        public int Level;
        public float Experience;
    }

    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/AttackData", order = 0)]
    public class AttackData : ScriptableObject
    {
        [Header("Display Info")]
        public string attackName;
        public Vector2Int AttackPowerBaseRange;
        public AttackType AttackType;
        public TargetType TargetType;
        public float cooldownTime;
    }
}