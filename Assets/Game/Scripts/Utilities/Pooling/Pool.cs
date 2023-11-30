using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Utilities.Pooling
{
    [Serializable]
    public class Pool
    {
        private static Vector3 FarAway = new Vector3(1000, 1000, 1000);
        public PoolInfo poolInfo;
        public List<GameObject> Pooled;
        public List<GameObject> InUse;

        public GameObject SamplePrefab
        {
            get
            {
                if (poolInfo.Prefab != null)
                    return poolInfo.Prefab;
                else
                    return InUse[0];
            }
        }

        public void SetPoolInfo(PoolInfo info)
        {
            poolInfo = info;
        }

        public GameObject Extend()
        {
            GameObject tempObject = null;

            switch (poolInfo.ExtendModel)
            {
                case PoolInfo.ExtendType.Never:
                    break;
                case PoolInfo.ExtendType.ForceCreate:
                    tempObject = Object.Instantiate(SamplePrefab, FarAway, Quaternion.identity);
                    PoolObject poolObject = tempObject.GetComponent<PoolObject>();
                    if (poolObject == null)
                    {
                        poolObject = tempObject.AddComponent<PoolObject>();
                    }

                    poolObject.SetPool(this);
                    poolObject.Reset();
                    InUse.Add(tempObject);
                    break;
                case PoolInfo.ExtendType.ForceRotate:
                    tempObject = InUse[0];
                    tempObject.GetComponent<PoolObject>().Reset();
                    InUse.Remove(tempObject);
                    InUse.Add(tempObject);
                    break;
            }

            return tempObject;
        }

        public GameObject Fetch(bool isActive)
        {
            GameObject toReturn;
            if (Pooled.Count > 0)
            {
                toReturn = Pooled[0];
                Pooled.Remove(toReturn);
                InUse.Add(toReturn);
            }
            else
                toReturn = Extend();

            if (toReturn != null)
            {
                if (isActive)
                    toReturn.SetActive(true);

                toReturn.transform.parent = null;
            }

            return toReturn;
        }


        public void CreateObjects()
        {
            Pooled = new List<GameObject>();
            InUse = new List<GameObject>();

            for (int i = 0; i < poolInfo.initSize; i++)
            {
                Pooled.Add(Object.Instantiate(poolInfo.Prefab));

                PoolObject poolObject = Pooled[i].GetComponent<PoolObject>();
                if (poolObject == null)
                    poolObject = Pooled[i].AddComponent<PoolObject>();
                Pooled[i].GetComponent<IPoolObject>().OnCreate();
                poolObject.gameObject.SetActive(false);
                poolObject.gameObject.transform.position = FarAway;
                poolObject.SetPool(this);

                if (!PoolManager.instance.SetParentNull)
                    poolObject.transform.parent = PoolManager.instance.transform;
            }
        }

        public void Release(PoolObject poolObject)
        {
            Release(poolObject.gameObject);
        }

        public void Release(GameObject poolObject)
        {
            if (poolObject == null)
                return;
            poolObject.SetActive(false);

            if (!PoolManager.instance.SetParentNull)
                poolObject.transform.SetParent(PoolManager.instance.transform);

            InUse.Remove(poolObject);
            Pooled.Add(poolObject);
        }

        public void ReleaseAll()
        {
            while (InUse.Count > 0)
            {
                Release(InUse[0]);
            }
        }
    }
}