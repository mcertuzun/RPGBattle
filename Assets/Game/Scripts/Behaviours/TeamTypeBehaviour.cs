using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Behaviours
{
    public class TeamTypeBehaviour : MonoBehaviour
    {
        public TeamType currentTeamType = TeamType.Ally;
        public AttackSignals attackSignals;

        public delegate void OnTeamTypeChanged(TeamType teamType);
        public event OnTeamTypeChanged TeamTypeChangedEvent;
        private void AttackSignalsOnAttackEndEvent()
        {
            TeamTypeChangedEvent?.Invoke(SetNextTeamType());
        }

        private TeamType SetNextTeamType()
        {
            currentTeamType = currentTeamType == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
            return currentTeamType;
        }
        
        private void OnEnable()=> attackSignals.AttackEndEvent += AttackSignalsOnAttackEndEvent;
        private void OnDisable() => attackSignals.AttackEndEvent -= AttackSignalsOnAttackEndEvent;

    }
}