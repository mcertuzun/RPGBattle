namespace Game.Scripts.Utilities.UI
{
    public interface IStateListener
    {
        void StartListen();
        void StopListen();
        void OnStateChange(GameState gameState);
    }
}