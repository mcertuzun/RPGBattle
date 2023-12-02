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
            PlayTween();
        }

        private void PlayTween()
        {
            var originalColor = textDisplay.color;
            if (DOTween.IsTweening(textDisplay)) return;
            textDisplay.DOColor(Color.red, 0.5f).OnComplete(() => { textDisplay.DOColor(originalColor, 0.5f); })
                .SetId("ColorChange");
            textDisplay.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 1);
        }
    }
}