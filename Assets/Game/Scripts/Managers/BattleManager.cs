using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Scripts.Managers
{
    public class BattleManager : MonoBehaviour
    {
        [FormerlySerializedAs("teamController")] [SerializeField]
        private TeamManager teamManager;
        
        public int battleRound
        {
            get
            {
                _battleRound = PlayerPrefs.GetInt(nameof(battleRound), 0);
                return _battleRound;
            }
            set
            {
                _battleRound = value;
                PlayerPrefs.SetInt(nameof(battleRound), _battleRound);
            }
        }

        private int _battleRound;

        [Header("Events")] public UnityEvent<int> OnNextBattle;
   
        //TODO: Just create AIController
        [Header("AI Settings")] public TeamType currentTeamType = TeamType.Ally;
        public AttackSignals attackSignals;

        public void Setup()
        {
            teamManager.Setup();
            SetupAI();
        }

        private void SetupAI()
        {
            currentTeamType = TeamType.Ally;
            attackSignals.canPlayerHit = true;
        }

        private void OnAttackFinished()
        {
            SetNextTeamType();
            if (currentTeamType == TeamType.Enemy)
            {
                teamManager.GetRandomEnemy().Hit(false);
            }
        }

        public void OnBattleEnd()
        {
            battleRound++;
            OnNextBattle?.Invoke(battleRound);
        }
        public void OnResetBattle()
        {
            battleRound = 0;
            OnNextBattle?.Invoke(battleRound);
        }

        private void SetNextTeamType()
        {
            currentTeamType = currentTeamType == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
        }

        private void OnEnable() => attackSignals.AttackEndEvent += OnAttackFinished;
        private void OnDisable() => attackSignals.AttackEndEvent -= OnAttackFinished;
    }
}