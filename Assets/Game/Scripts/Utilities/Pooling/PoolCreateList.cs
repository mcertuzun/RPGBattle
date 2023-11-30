using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utilities.Pooling
{
    [CreateAssetMenu(fileName = "PoolCreateInfo", menuName = "Scriptables/Pooling/PoolCreateInfo")]
    public class PoolCreateList : ScriptableObject
    {
        public List<PoolInfo> CreationList;
    }
}