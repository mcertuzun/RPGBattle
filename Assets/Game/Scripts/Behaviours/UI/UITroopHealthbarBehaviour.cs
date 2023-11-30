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
            int totalHealth = healthBehaviour.GetCurrentHealth();
            healthSlider.SetupDisplay((float)totalHealth);
            UpdateHealthDisplay(totalHealth);
        }

        private void UpdateHealthDisplay(int newHealthAmount)
        {
            healthSlider.SetCurrentValue(newHealthAmount);
        }
    }
}