namespace Game.Scripts.Utilities.Pooling
{
    public interface IPoolObject
    {
        void ClearForRelease();
        void ResetForRotate();
        void OnCreate();
    }
}