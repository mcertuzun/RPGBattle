using System.Collections.Generic;
using Game.Scripts.Controllers;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Teams")] [SerializeField] private List<TroopControllerBase> allyTroops;
        [SerializeField] private List<TroopControllerBase> enemyTroops;
        private Dictionary<TeamType, List<TroopControllerBase>> aliveTroops;

        //TODO: Review
        [Header("AI Settings")] public TeamType currentTeamType = TeamType.Ally;
        public AttackSignals attackSignals;

        [Header("UI Selection")] [SerializeField]
        private UISelectionController uiSelectionController;

        public List<Transform> TroopPositions;
        // [Header("Win Battle")] public SceneTimelineBehaviour victoryCutsceneBehaviour;
        // [Header("Lose Battle")] public SceneTimelineBehaviour defeatCutsceneBehaviour;

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
            CreateAllyTroops();
            aliveTroops.Add(TeamType.Enemy, new List<TroopControllerBase>());
            aliveTroops[TeamType.Enemy].AddRange(enemyTroops);

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

        private void SetTroops(TeamType teamType)
        {
            for (int i = 0; i < aliveTroops[teamType].Count; i++)
            {
                aliveTroops[teamType][i].SetTroopData();
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
            //Victory UI.
            StopAllAliveTeamTroops(aliveTroops[TeamType.Ally]);
            Debug.Log("Victory");
        }

        private void SetBattleDefeat()
        {
            //Defeat UI
            StopAllAliveTeamTroops(aliveTroops[TeamType.Enemy]);
            Debug.Log("Defeat");
        }

        void RemoveTroopFromTargets(TroopControllerBase deadTroop)
        {
            var otherTeam = TeamType.Ally;
            if (deadTroop.data.teamType == TeamType.Ally)
                otherTeam = TeamType.Enemy;
            foreach (var troop in aliveTroops[otherTeam])
            {
                troop.RemoveTargetTroop(deadTroop);
            }
        }

        private void StopAllAliveTeamTroops(List<TroopControllerBase> aliveTeamTroops)
        {
            for (int i = 0; i < aliveTeamTroops.Count; i++)
            {
                // aliveTeamTroops[i].BattleEnded();
            }
        }

        private void SetNextTeamType()
        {
            currentTeamType = currentTeamType == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
        }

        private void OnEnable() => attackSignals.AttackEndEvent += OnAttackFinished;
        private void OnDisable() => attackSignals.AttackEndEvent -= OnAttackFinished;
    }
}