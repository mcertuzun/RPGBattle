using TMPro;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class UITextBehaviour : MonoBehaviour
    {
        [Header("Component Reference")]
        public TextMeshProUGUI textDisplay;

        public void SetText(string newText)
        {
            textDisplay.SetText(newText);
        }
    }
}