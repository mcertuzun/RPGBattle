using UnityEngine;

namespace Game.Scripts.Utilities.Pooling
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlePoolObject : MonoBehaviour, IPoolObject
    {
        private void Start()
        {
            var particleSystem = GetComponent<ParticleSystem>();
            var main = particleSystem.main;
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
        }
    }
}