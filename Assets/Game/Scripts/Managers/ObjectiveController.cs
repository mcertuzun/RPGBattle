using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Scripts.Managers
{
    public class ObjectiveController : MonoBehaviour
    {
        [FormerlySerializedAs("teamController")] [SerializeField]
        private TeamManager teamManager;

        public UnityEvent OnVictoryEvent, OnDefeatEvent;

        private void OnEnemyTroopEliminated(int count)
        {
            if (CheckObjective(count))
            {
                OnVictoryEvent?.Invoke();
            }
        }

        private void OnAllyTroopEliminated(int count)
        {
            if (CheckObjective(count))
            {
                OnDefeatEvent?.Invoke();
            }
        }

        private bool CheckObjective(int count)
        {
            return count <= 0;
        }

        public void OnEnable()
        {
            teamManager.OnAllyTroopEliminated += OnAllyTroopEliminated;
            teamManager.OnEnemyTroopEliminated += OnEnemyTroopEliminated;
        }

        private void OnDisable()
        {
            teamManager.OnAllyTroopEliminated -= OnAllyTroopEliminated;
            teamManager.OnEnemyTroopEliminated -= OnEnemyTroopEliminated;
        }
    }
}