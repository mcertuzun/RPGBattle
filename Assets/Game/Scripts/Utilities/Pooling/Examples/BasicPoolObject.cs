using UnityEngine;

namespace Assets.Library.Pooling
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