using Game.Scripts.Behaviours.Troop;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class UITroopHealthbarBehaviour : MonoBehaviour
    {
        [Header("Unit Health")] public TroopHealthBehaviour healthBehaviour;

        [Header("UI References")] public UIImageBehaviour healthSlider;

        private void OnEnable()
        {
            healthBehaviour.HealthChangedEvent += UpdateHealthDisplay;
            healthBehaviour.HealthSetup += SetupHealth;
        }

        private void OnDisable()
        {
            healthBehaviour.HealthChangedEvent -= UpdateHealthDisplay;
            healthBehaviour.HealthSetup -= SetupHealth;
        }

        public void SetupHealth(float newHealthAmount) => healthSlider.SetHealth(newHealthAmount);

        private void UpdateHealthDisplay(float newHealthAmount)
        {
            healthSlider.UpdateDisplay(newHealthAmount);
        }
    }
}