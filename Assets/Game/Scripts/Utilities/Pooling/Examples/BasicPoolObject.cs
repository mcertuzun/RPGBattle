using Game.Scripts.Utilities.Pooling.Core;
using UnityEngine;

namespace Game.Scripts.Utilities.Pooling.Examples
{
    public class BasicPoolObject : MonoBehaviour, IPoolObject
    {
        public void ClearForRelease()
        {
        }

        public void OnCreate()
        {
        }

        public void ResetForRotate()
        {
        }
    }
}