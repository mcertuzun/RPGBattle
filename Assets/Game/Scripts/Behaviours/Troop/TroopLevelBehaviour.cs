using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopLevelBehaviour : MonoBehaviour
    {
        [Header("Level Info")] [SerializeField] [ReadOnly]
        private int currentLevel;

        public delegate void LevelChangedEventHandler(int newLevel);

        public event LevelChangedEventHandler LevelChangedEvent;

        public void SetupCurrentLevel(int newLevel)
        {
            this.currentLevel = newLevel;
        }

        public void ChangeLevel(int newLevel)
        {
            this.currentLevel = newLevel;
            LevelChangedEvent?.Invoke(newLevel);
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }
    }
}