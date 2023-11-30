using Game.Scripts.Utilities.UI;

namespace Game.Scripts.Utilities.StateSystem
{
    public interface IStateListener
    {
        void StartListen();
        void StopListen();
        void OnStateChange(UIState uiState);
    }
}