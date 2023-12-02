using Game.Scripts.Controllers.Troop;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "SO/TroopData", order = 0)]
    public class TroopData : ScriptableObject
    {
        //Todo seperate into different data holders.
        public string Name;
        public int Health;
        public float AttackPower;
        public int Experience;
        public int Level;
        public TeamType teamType;
        public AttackType AttackType;
        public GameObject troopPrefab;

        public TroopControllerBase Create(Transform troopPosition)
        {
            var troop = Instantiate(troopPrefab, troopPosition.position, Quaternion.identity, troopPosition)
                .GetComponent<TroopControllerBase>();
            return troop;
        }
        public void GainExperience(int expGain)
        {
            Experience += expGain;
            while (Experience >= 5)
            {
                LevelUp();
                Experience -= 5;
            }
        }
        private void LevelUp()
        {
            Level++;
            AttackPower = Mathf.CeilToInt(AttackPower * 1.1f);
            Health = Mathf.CeilToInt(Health * 1.1f);
        }

    }

    public enum TeamType
    {
        Ally,
        Enemy
    }

    public enum AttackType
    {
        Solo,
        Area
    }
}