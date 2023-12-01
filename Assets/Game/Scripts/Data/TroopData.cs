using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "SO/TroopData", order = 0)]
    public class TroopData : ScriptableObject
    {
        //name, health, attack power, experience and level.
        public string troopName;
        public int health;
        public float attackPower;
        public float experience;
        public float level;
        public TeamType teamType;
        public AttackType AttackType;
    }

    public enum TeamType
    {
        Ally,Enemy
    }
    public enum AttackType
    {
        Solo,
        Area
    }
  
}