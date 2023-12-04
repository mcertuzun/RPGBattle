using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class ObjectiveController : MonoBehaviour
    {
        [SerializeField] private TeamController teamController;
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

        public void Setup()
        {
            teamController.OnAllyTroopEliminated += OnAllyTroopEliminated;
            teamController.OnEnemyTroopEliminated += OnEnemyTroopEliminated;
        }

        private void OnDisable()
        {
            teamController.OnAllyTroopEliminated -= OnAllyTroopEliminated;
            teamController.OnEnemyTroopEliminated -= OnEnemyTroopEliminated;
        }
    }
}