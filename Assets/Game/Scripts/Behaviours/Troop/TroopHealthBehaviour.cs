using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopHealthBehaviour : MonoBehaviour
    {
        [Header("Health Info")] [SerializeField]
        private float currentHealth;

        public bool unitIsAlive => currentHealth > 0;

        [Header("Events")] public UnityEvent<float> healthDifferenceEvent;
        public UnityEvent healthIsZeroEvent;

        public delegate void HealthChangedEventHandler(float newHealthAmount);

        public event HealthChangedEventHandler HealthChangedEvent;
        public event HealthChangedEventHandler HealthSetup;

        public void SetupCurrentHealth(float totalHealth)
        {
            currentHealth = totalHealth;
            HealthSetup?.Invoke(currentHealth);
        }

        public void ChangeHealth(float healthDifference)
        {
            currentHealth -= healthDifference;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                HealthIsZeroEvent();
            }

            healthDifferenceEvent.Invoke(healthDifference);
            HealthChangedEvent?.Invoke(currentHealth);
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        void HealthIsZeroEvent()
        {
            healthIsZeroEvent.Invoke();
        }
    }
}