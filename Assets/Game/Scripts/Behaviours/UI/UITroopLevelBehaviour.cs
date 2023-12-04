using Game.Scripts.Behaviours.Troop;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI
{
    public class UITroopLevelBehaviour : MonoBehaviour
    {
        [Header("Unit Level")] public TroopLevelBehaviour LevelBehaviour;

        [Header("UI References")] public UITextBehaviour levelText;

        private void OnEnable() => LevelBehaviour.LevelChangedEvent += UpdateLevel;
        private void OnDisable() => LevelBehaviour.LevelChangedEvent -= UpdateLevel;

        private void UpdateLevel(int newLevel)
        {
            levelText.SetText(newLevel.ToString());
        }
    }
}