using Game.Scripts.Utilities.Pooling.Core;
using UnityEngine;

namespace Game.Scripts.Utilities.Pooling.Examples
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlePoolObject : MonoBehaviour, IPoolObject
    {
        private void Start()
        {
            var _ps = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = _ps.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
        public void ClearForRelease()
        {
            GetComponent<ParticleSystem>().Stop();
        }

        public void ResetForRotate()
        {
            GetComponent<ParticleSystem>().Stop();
        }

        private void OnParticleSystemStopped()
        {
            if (GetComponent<PoolObject>() != null)
                GetComponent<PoolObject>().Release();
        }

        public void OnCreate()
        {
            // throw new System.NotImplementedException();
        }
    }
}