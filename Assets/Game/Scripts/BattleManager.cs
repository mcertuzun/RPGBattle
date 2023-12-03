using System.Collections.Generic;
using Game.Scripts.Controllers;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using Game.Scripts.Utilities;
using Game.Scripts.Utilities.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Teams")] [SerializeField] private List<TroopControllerBase> allyTroops;
        [SerializeField] private List<TroopControllerBase> enemyTroops;
        [SerializeField] private SerializableDictionary<TeamType, List<TroopControllerBase>> aliveTroops;
        public TroopDataList allTroops;

        [Header("AI Settings")] public TeamType currentTeamType = TeamType.Ally;
        public AttackSignals attackSignals;

        [Header("UI Selection")] [SerializeField]
        private UISelectionController uiSelectionController;

        [SerializeField] private List<Transform> TroopPositions;
        [SerializeField] private UIStateController uiStateController;

        public int battleRound
        {
            get
            {
                PlayerPrefs.GetInt(nameof(battleRound), 0);
                return _battleRound;
            }
            set
            {
                uiSelectionController.selectedUITroops.ForEach(x => x.troopData.GainExperience(1));
                _battleRound = value;
                PlayerPrefs.SetInt(nameof(battleRound), _battleRound);
            }
        }

        private int _battleRound;


        public void SetupTeams()
        {
            CreateAliveTroops();
            AutoAssignTroopTeamTargets();
        }

        private void OnAttackFinished()
        {
            SetNextTeamType();
            if (currentTeamType == TeamType.Enemy)
            {
                var randomEnemy = aliveTroops[currentTeamType][Random.Range(0, aliveTroops[currentTeamType].Count)];
                randomEnemy.Hit(false);
            }
        }

        private void CreateAliveTroops()
        {
            aliveTroops ??= new();
            aliveTroops.Add(TeamType.Ally, new List<TroopControllerBase>());
            aliveTroops.Add(TeamType.Enemy, new List<TroopControllerBase>());
            CreateAllTroops();
            SetTroops(TeamType.Ally);
            SetTroops(TeamType.Enemy);
        }

        private void CreateAllyTroops()
        {
            for (var i = 0; i < uiSelectionController.selectedUITroops.Count; i++)
            {
                var createdTroop = uiSelectionController.selectedUITroops[i].troopData.Create(TroopPositions[i]);
                aliveTroops[TeamType.Ally].Add(createdTroop);
            }
        }

        private void CreateAllTroops()
        {
            CreateAllyTroops();
            aliveTroops[TeamType.Enemy].Add(allTroops.Value[^1].Create(TroopPositions[^1]));
        }

        private void SetTroops(TeamType teamType)
        {
            for (int i = 0; i < aliveTroops[teamType].Count; i++)
            {
                aliveTroops[teamType][i].SetHealth();
                aliveTroops[teamType][i].TroopDiedEvent += TroopHasDied;
            }
        }

        private void AutoAssignTroopTeamTargets()
        {
            for (int i = 0; i < aliveTroops[TeamType.Ally].Count; i++)
            {
                aliveTroops[TeamType.Ally][i].AssignTargetTroops(aliveTroops[TeamType.Enemy]);
            }

            for (int i = 0; i < aliveTroops[TeamType.Enemy].Count; i++)
            {
                aliveTroops[TeamType.Enemy][i].AssignTargetTroops(aliveTroops[TeamType.Ally]);
            }
        }

        private void TroopHasDied(TroopControllerBase deadTroop)
        {
            RemoveTroopFromAliveTroops(deadTroop);
        }

        private void RemoveTroopFromAliveTroops(TroopControllerBase deadTroop)
        {
            CheckRemainingTeams();

            if (aliveTroops[deadTroop.data.teamType].Contains(deadTroop))
            {
                deadTroop.TroopDiedEvent -= TroopHasDied;
                RemoveTroopFromTargets(deadTroop);
                aliveTroops[deadTroop.data.teamType].Remove(deadTroop);
                Destroy(deadTroop.gameObject);
            }

            CheckRemainingTeams();
        }

        void CheckRemainingTeams()
        {
            if (aliveTroops[TeamType.Ally].Count == 0)
            {
                SetBattleDefeat();
            }
            else if (aliveTroops[TeamType.Enemy].Count == 0)
            {
                SetBattleVictory();
            }
            //TODO: Check draw condition later
        }

        private void SetBattleVictory()
        {
            uiStateController.SetState((int)UIState.VictoryUI);
        }

        private void SetBattleDefeat()
        {
            uiStateController.SetState((int)UIState.DefeatUI);
        }

        public void NextLevel()
        {
            uiStateController.SetState((int)UIState.SelectTroopsUI);
            battleRound++;
            StopAllAliveTeamTroops();
            currentTeamType = TeamType.Ally;
            attackSignals.canPlayerHit = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void RemoveTroopFromTargets(TroopControllerBase deadTroop)
        {
            var otherTeam = TeamType.Ally;
            if (deadTroop.data.teamType == TeamType.Ally)
                otherTeam = TeamType.Enemy;
            foreach (var troop in aliveTroops[otherTeam])
            {
                troop.RemoveTargetTroop(deadTroop);
            }
        }

        private void StopAllAliveTeamTroops()
        {
            for (var i = 0; i < aliveTroops[TeamType.Ally].Count; i++)
            {
                aliveTroops[TeamType.Ally][i].ResetTroop();
                aliveTroops[TeamType.Ally][i].TroopDiedEvent -= TroopHasDied;
                Destroy(aliveTroops[TeamType.Ally][i].gameObject);
            }

            aliveTroops.Clear();
        }

        private void SetNextTeamType()
        {
            currentTeamType = currentTeamType == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
        }

        private void OnEnable() => attackSignals.AttackEndEvent += OnAttackFinished;
        private void OnDisable() => attackSignals.AttackEndEvent -= OnAttackFinished;
    }
}