using UnityEngine;

namespace Game.Scripts.Utilities.Pooling
{
    public class BasicPoolObject : MonoBehaviour, IPoolObject
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale = Vector3.one;

        public void ClearForRelease()
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = scale;
        }

        public void OnCreate()
        {
        }

        public void ResetForRotate()
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = scale;
        }

        void Start()
        {
            scale = transform.localScale;
            position = transform.localPosition;
            rotation = transform.localRotation;
        }
    }
}