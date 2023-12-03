using Game.Scripts.Controllers.Troop;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "SO/TroopData", order = 0)]
    public class TroopData : ScriptableObject
    {
        //Todo seperate into different data holders.
        public string Name;
        public float Health;
        public float AttackPower;
        public int Experience;
        public int Level;
        public TeamType teamType;
        public GameObject troopPrefab;

        public TroopControllerBase Create(Transform troopPosition)
        {
            var troop = Instantiate(troopPrefab, troopPosition.position, Quaternion.identity, troopPosition)
                .GetComponent<TroopControllerBase>();
            troop.data.Load();
            return troop;
        }


        public void GainExperience(int expGain)
        {
            Experience += expGain;
            if (Experience >= 5)
            {
                LevelUp();
                Experience -= 5;
            }
            Save();
        }

        private void LevelUp()
        {
            Level++;
            AttackPower *= 1.1f;
            Health *= 1.1f;
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt(Name + nameof(Experience), Experience);
            PlayerPrefs.SetFloat(Name + nameof(AttackPower), AttackPower);
            PlayerPrefs.SetFloat(Name + nameof(Health), Health);
        }

        private void Load()
        {
            Experience = PlayerPrefs.GetInt(Name + nameof(Experience), 0);
            AttackPower = PlayerPrefs.GetFloat(Name + nameof(AttackPower), 10);
            Health = PlayerPrefs.GetFloat(Name + nameof(Health), 100);
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