using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Behaviours.UI
{
    public class UISliderBehaviour : MonoBehaviour
    {
        [Header("UI References")] public Slider slider;
        public UITextBehaviour textSliderDisplay;

        public void SetupDisplay(float totalValue)
        {
            SetMaxValue(totalValue);
        }

        void SetMaxValue(float newValue)
        {
            slider.maxValue = newValue;
        }

        public void SetCurrentValue(float newValue)
        {
            slider.value = newValue;
            SetTextDisplay();
        }

        void SetTextDisplay()
        {
            if (textSliderDisplay != null)
            {
                textSliderDisplay.SetText(slider.value + "/" + slider.maxValue);
            }
        }
    }
}