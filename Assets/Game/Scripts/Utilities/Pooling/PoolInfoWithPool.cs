using System;
using UnityEngine;

namespace Game.Scripts.Utilities.Pooling
{
    [CreateAssetMenu(fileName = "PoolInfoWithPool", menuName = "Scriptables/Pooling/PoolInfoWithPool")]
    [Serializable]
    public class PoolInfoWithPool : PoolInfo
    {
        public int poolIndex;

        public GameObject Fetch(bool isActive = false)
        {
            return PoolManager.FetchByIndex(poolIndex, isActive);
        }
    }
}