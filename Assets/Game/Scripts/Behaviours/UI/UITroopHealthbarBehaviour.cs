using Game.Scripts.Behaviours.Troop;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class UITroopHealthbarBehaviour : MonoBehaviour
    {
        [Header("Unit Health")] public TroopHealthBehaviour healthBehaviour;

        [Header("UI References")] public UIImageBehaviour healthSlider;

        private void OnEnable() => healthBehaviour.HealthChangedEvent += UpdateHealthDisplay;
        private void OnDisable() => healthBehaviour.HealthChangedEvent -= UpdateHealthDisplay;
        private void Start() => SetupHealthDisplay();

        private void SetupHealthDisplay()
        {
            
            float totalHealth = healthBehaviour.GetCurrentHealth();
            healthSlider.SetupDisplay(totalHealth);
            UpdateHealthDisplay(totalHealth);
        }

        private void UpdateHealthDisplay(float newHealthAmount)
        {
            healthSlider.SetCurrentValue(newHealthAmount);
        }
    }
}