using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Utilities.Pooling.Core
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;
        public List<PoolInfo> CreationList;
        public List<Pool> pools;
        private Hashtable PoolByName;
        public bool createOnStart;

        public bool SetParentNull = true;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (createOnStart)
            {
                CreatePools();
            }
        }

        private static void CreatePools()
        {
            instance.pools ??= new List<Pool>();
            instance.PoolByName ??= new Hashtable();

            foreach (var poolInfo in instance.CreationList)
                CreatePoolInternal(poolInfo);
        }

        private static void CreatePoolInternal(PoolInfo poolInfo)
        {
            Pool TempPool = new Pool();
            TempPool.SetPoolInfo(poolInfo);
            if (instance.PoolByName.ContainsKey(poolInfo.PoolName))
            {
                ((Pool)instance.PoolByName[poolInfo.PoolName]).ReleaseAll();
                instance.PoolByName[poolInfo.PoolName] = TempPool;
            }
            else
            {
                instance.PoolByName.Add(poolInfo.PoolName, TempPool);
                instance.pools.Add(TempPool);
            }

            TempPool.CreateObjects();
        }

        public static void ReleaseAll()
        {
            foreach (Pool pool in instance.pools)
            {
                pool.ReleaseAll();
            }
        }

        public static bool IsCreated
        {
            get { return instance != null; }
        }

        public static GameObject FetchByIndex(int itemIndex, bool isActive = false)
        {
            return instance.pools[itemIndex].Fetch(isActive);
        }

        public static GameObject Fetch(string itemName, bool isActive = false)
        {
            Pool pool = (Pool)instance.PoolByName[itemName];
            return pool.Fetch(isActive);
        }
        public static GameObject Fetch(string itemName, Vector3 position, bool isActive = false)
        {
            GameObject go = Fetch(itemName, isActive);
            go.transform.SetPositionAndRotation(position, Quaternion.identity);

            return go;
        }
        public static GameObject Fetch(string itemName, Vector3 position, Quaternion rotation, bool isActive = false)
        {
            GameObject go = Fetch(itemName, isActive);
            go.transform.SetPositionAndRotation(position, rotation);

            return go;
        }

        public static GameObject Fetch(string itemName, Vector3 position, Vector3 rotation, bool isActive = false)
        {
            return Fetch(itemName, position, Quaternion.Euler(rotation), isActive);
        }

        public static GameObject Fetch(string itemName, Vector3 position, Vector3 rotation, Transform parent,
            bool isActive = false)
        {
            GameObject go = Fetch(itemName, position, Quaternion.Euler(rotation), isActive);
            go.transform.SetParent(parent);

            return go;
        }
    }
}