using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class BattleStateBehaviour : MonoBehaviour
    {
        public TeamType currentTeamState = TeamType.Ally;
        public AttackSignals attackSignals;

        public delegate void OnTeamTypeChanged(TeamType teamType);

        public event OnTeamTypeChanged TeamTypeChangedEvent;

        private void OnEnable()
        {
            attackSignals.AttackEndEvent += AttackSignalsOnAttackEndEvent;
        }

        private void OnDisable()
        {
            attackSignals.AttackEndEvent -= AttackSignalsOnAttackEndEvent;
        }

        private void AttackSignalsOnAttackEndEvent()
        {
            TeamTypeChangedEvent?.Invoke(SetNextTeamType());
        }

        private TeamType SetNextTeamType()
        {
            currentTeamState = currentTeamState == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
            return currentTeamState;
        }
    }
}