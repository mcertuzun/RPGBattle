using UnityEngine;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopLevelBehaviour : MonoBehaviour
    {
        [Header("Level Info")] [SerializeField]
        private int Level;

        public delegate void LevelChangedEventHandler(int newLevel);

        public event LevelChangedEventHandler LevelChangedEvent;

        public void SetupCurrentLevel(int dataLevel)
        {
            Level = dataLevel;
            LevelChangedEvent?.Invoke(Level);
        }
    }
}