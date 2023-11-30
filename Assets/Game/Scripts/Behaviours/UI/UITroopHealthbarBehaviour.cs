using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class UITroopHealthbarBehaviour : MonoBehaviour
    {
        [Header("Unit Health")] public TroopHealthBehaviour healthBehaviour;

        [Header("UI References")] public UISliderBehaviour healthSlider;

        void OnEnable() => healthBehaviour.HealthChangedEvent += UpdateHealthDisplay;
        void OnDisable() => healthBehaviour.HealthChangedEvent -= UpdateHealthDisplay;
        void Start() => SetupHealthDisplay();

        void SetupHealthDisplay()
        {
            int totalHealth = healthBehaviour.GetCurrentHealth();
            healthSlider.SetupDisplay((float)totalHealth);
            UpdateHealthDisplay(totalHealth);
        }

        private void UpdateHealthDisplay(int newhealthamount)
        {
            healthSlider.SetCurrentValue(newhealthamount);
        }
    }
}