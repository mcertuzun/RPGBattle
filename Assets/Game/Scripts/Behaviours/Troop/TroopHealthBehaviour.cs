using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopHealthBehaviour : MonoBehaviour
    {
        [Header("Health Info")] [SerializeField] [ReadOnly]
        private int currentHealth;

        public bool unitIsAlive => currentHealth > 0;

        [Header("Events")] public UnityEvent<int> healthDifferenceEvent;
        public UnityEvent healthIsZeroEvent;

        public delegate void HealthChangedEventHandler(int newHealthAmount);

        public event HealthChangedEventHandler HealthChangedEvent;

        public void SetupCurrentHealth(int totalHealth)
        {
            currentHealth = totalHealth;
        }

        public void ChangeHealth(int healthDifference)
        {
            currentHealth = currentHealth + healthDifference;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                HealthIsZeroEvent();
            }

            healthDifferenceEvent.Invoke(healthDifference);
            HealthChangedEvent?.Invoke(currentHealth);
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        void HealthIsZeroEvent()
        {
            healthIsZeroEvent.Invoke();
        }
    }
}