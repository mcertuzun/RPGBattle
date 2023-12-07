using System;
using Game.Scripts.Controllers.Troop;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "SO/TroopData", order = 0)]
    public class TroopData : ScriptableObject
    {
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
            }
        }

        public void LevelUp()
        {
            Level++;
            AttackPower *= 1.1f;
            Health *= 1.1f;
            Experience -= 5;
            Save();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(Name + nameof(Experience), Experience);
            PlayerPrefs.SetInt(Name + nameof(Level), Level);
            PlayerPrefs.SetFloat(Name + nameof(AttackPower), AttackPower);
            PlayerPrefs.SetFloat(Name + nameof(Health), Health);
        }

        public void Load()
        {
            Experience = PlayerPrefs.GetInt(Name + nameof(Experience), 0);
            AttackPower = PlayerPrefs.GetFloat(Name + nameof(AttackPower), 10);
            Health = PlayerPrefs.GetFloat(Name + nameof(Health), 100);
            Level = PlayerPrefs.GetInt(Name + nameof(Level), Level);
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(Name + nameof(Experience));
            PlayerPrefs.DeleteKey(Name + nameof(AttackPower));
            PlayerPrefs.DeleteKey(Name + nameof(Health));
            PlayerPrefs.DeleteKey(Name + nameof(Level));
        }

        public override string ToString()
        {
            return
                $"{base.ToString()}, {nameof(Name)}: {Name}, {nameof(Health)}: {Health}, {nameof(AttackPower)}: {AttackPower}, {nameof(Experience)}: {Experience}, {nameof(Level)}: {Level}";
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