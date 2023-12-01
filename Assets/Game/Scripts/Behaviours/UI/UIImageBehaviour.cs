using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Behaviours.UI
{
    public class UIImageBehaviour : MonoBehaviour
    {
        [Header("UI References")] public Image slider;
        public UITextBehaviour textSliderDisplay;
        private int maxHealth;

        public void SetupDisplay(float totalValue)
        {
            SetMaxValue(totalValue);
        }

        void SetMaxValue(float newValue)
        {
            slider.fillAmount = newValue;
            maxHealth = (int)newValue;
        }

        public void SetCurrentValue(float newValue)
        {
            Debug.Log(newValue);
            slider.fillAmount = newValue/100;
            SetTextDisplay();
        }

        private void SetTextDisplay()
        {
            if (textSliderDisplay != null)
            {
                textSliderDisplay.SetText(slider.fillAmount * 100 + "/" + maxHealth);
            }
        }
    }
}