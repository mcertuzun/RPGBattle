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

        private void UpdateHealthDisplay(float newHealthAmount)
        {
            healthSlider.SetupDisplay(newHealthAmount);
            healthSlider.SetCurrentValue(newHealthAmount);
        }
    }
}