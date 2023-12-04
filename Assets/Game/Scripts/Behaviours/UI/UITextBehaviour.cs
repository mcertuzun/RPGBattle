using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class UITextBehaviour : MonoBehaviour
    {
        [Header("Component Reference")] public TextMeshProUGUI textDisplay;

        public void SetText(string newText)
        {
            textDisplay.SetText(newText);
        }

        public void AlertText()
        {
            PlayAlertTween();
        }

        private void PlayAlertTween()
        {
            var originalColor = textDisplay.color;
            if (textDisplay != null && DOTween.IsTweening(textDisplay) || textDisplay == null) return;
            textDisplay.DOColor(Color.red, 0.5f).OnComplete(() => { textDisplay.DOColor(originalColor, 0.5f); });
            textDisplay.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 1);
        }
    }
}