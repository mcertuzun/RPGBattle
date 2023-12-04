namespace Game.Scripts.Utilities.Pooling.Core
{
    public interface IPoolObject
    {
        void ClearForRelease();
        void ResetForRotate();
        void OnCreate();

    }
}