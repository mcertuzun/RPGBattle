using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Behaviours.UI.TroopSelection
{
    public class UIPopUpWaitBehaviour : MonoBehaviour
    {
        [SerializeField] private Image loadImage;

        public void Open()
        {
            ResetAmount();
            loadImage.enabled = true;
        }

        public void Close()
        {
            ResetAmount();
            loadImage.enabled = false;
        }

        private void ResetAmount()
        {
            loadImage.fillAmount = 0;
        }

        public void Loading(Vector2 position, float timer)
        {
            loadImage.transform.position = position;
            loadImage.fillAmount = timer;
        }
    }
}