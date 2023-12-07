using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Behaviours.UI
{
    public class UIImageBehaviour : MonoBehaviour
    {
        [Header("UI References")] public Image slider;
        public UITextBehaviour textSliderDisplay;
        private int maxHealth;

        public void SetHealth(float newHealth)
        {
            maxHealth = (int)newHealth;
            UpdateDisplay(newHealth);
        }

        public void UpdateDisplay(float newHealth)
        {
            slider.fillAmount = newHealth / maxHealth;
            textSliderDisplay.SetText(newHealth.ToString("0.0"));
        }
    }
}