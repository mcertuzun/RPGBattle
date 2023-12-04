using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private TeamController teamController;
        [SerializeField] private ObjectiveController objectiveController;
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
        
        [Header("Events")]
        public UnityEvent<int> OnNextBattle;
        public UnityEvent OnStartBattle;
        public UnityEvent OnStopBattle;
        
        //TODO: Just create AIController
        [Header("AI Settings")] 
        public TeamType currentTeamType = TeamType.Ally;
        public AttackSignals attackSignals;
        
        public void Setup()
        {
            teamController.Setup();
            objectiveController.Setup();
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
                teamController.GetRandomEnemy().Hit(false);
            }
        }

        public void OnBattleEnd()
        {
            battleRound++;
        }
        
        public void NextBattle()
        {
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