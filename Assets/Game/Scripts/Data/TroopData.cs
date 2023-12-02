using Game.Scripts.Controllers.Troop;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "SO/TroopData", order = 0)]
    public class TroopData : ScriptableObject
    {
        //Todo separete into different data holders 
        public string Name;
        public int health;
        public float AttackPower;
        public float Experience;
        public float Level;
        public TeamType teamType;
        public AttackType AttackType;
        public GameObject troopPrefab;

        public TroopControllerBase Create(Transform troopPosition)
        {
            Debug.Log("Create VAR");
            var troop = Instantiate(troopPrefab, troopPosition.position, Quaternion.identity, troopPosition)
                .GetComponent<TroopControllerBase>();
            return troop;
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