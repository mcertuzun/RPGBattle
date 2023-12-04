using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]private TroopDataList aliveAllyList;
        [SerializeField]private int experiencePerBattle = 1;
        public void SetExperiences()
        {
            for (var i = 0; i < aliveAllyList.Value.Count; i++)
            {
                aliveAllyList.Value[i].GainExperience(experiencePerBattle);
            }
        }
    }
}