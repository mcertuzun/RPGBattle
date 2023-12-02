using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopDataList", menuName = "SO/TroopDataList", order = 0)]
    public class TroopDataList : ScriptableObject
    {
        public List<TroopData> Value;
    }
}