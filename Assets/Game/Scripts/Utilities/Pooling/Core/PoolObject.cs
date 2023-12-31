﻿using UnityEngine;

namespace Game.Scripts.Utilities.Pooling.Core
{
    public class PoolObject : MonoBehaviour
    {
        public Pool pool;

        IPoolObject poolClient
        {
            get
            {
                if (_poolClient == null)
                    _poolClient = GetComponent<IPoolObject>();
                return _poolClient;
            }
        }

        IPoolObject _poolClient;

        public void SetPool(Pool pool)
        {
            this.pool = pool;
        }

        public void Release()
        {
            if (poolClient != null)
                poolClient.ClearForRelease();
            pool.Release(this);
        }

        public void Reset()
        {
            if (poolClient != null)
                poolClient.ResetForRotate();
        }
    }
}