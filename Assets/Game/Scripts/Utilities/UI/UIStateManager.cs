using UnityEngine;

namespace Game.Scripts.Utilities.UI
{
    public class UIStateManager : MonoBehaviour
    {
        public UIStateController UIStateController;

        private void Awake() => Setup();

        private void Setup()
        {
            UIStateController.StartListen();
            UIStateController.SetState((int)UIState.SelectTroopsUI);
        }

        private void Destroy()
        {
            UIStateController.StopListen();
        }

        private void OnApplicationQuit() => Destroy();
    }
}