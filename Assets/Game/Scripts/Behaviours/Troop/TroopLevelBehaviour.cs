using Game.Scripts.Utilities.ReadOnlyDrawer;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop
{
    public class TroopLevelBehaviour : MonoBehaviour
    {
        [Header("Level Info")] [SerializeField] [ReadOnly]
        private int Level;
        [SerializeField] [ReadOnly]
        private int Experience;
        
        public delegate void LevelChangedEventHandler(int newLevel);

        public event LevelChangedEventHandler LevelChangedEvent;

        public void SetupCurrentLevel(int newLevel, int experience)
        {
            Level = newLevel;
            Experience = experience;
        }

        public void LevelUp()
        {
            this.Level++;
            LevelChangedEvent?.Invoke(Level);
        }
        
        public void GainExperience(int exp)
        {
            Experience += exp;
            if (Experience >= 5)
            {
                LevelUp();
                Experience -= 5;
            }
        }

        public int GetCurrentLevel()
        {
            return Level;
        }
    }
}