using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Managers
{
    public class TeamManager : MonoBehaviour
    {
        private Dictionary<TeamType, List<TroopControllerBase>> aliveTroops;
        private Dictionary<TeamType, List<TroopControllerBase>> createdTroops;
        [Header("Data")] [SerializeField] private TroopDataList allyTroops, enemyTroops, allAllyTroops;
        [SerializeField] private List<Transform> AllyPositions;
        [SerializeField] private List<Transform> EnemyPositions;
        [SerializeField] Transform unseenTransform;
        public event Action<int> OnAllyTroopEliminated;
        public event Action<int> OnEnemyTroopEliminated;

        #region Init

        private void Start()
        {
            InitTroops();
        }

        private void InitTroops()
        {
            InitDictionary(ref aliveTroops);
            InitDictionary(ref createdTroops);
            CreateAllTroops();
        }

        private void InitDictionary(ref Dictionary<TeamType, List<TroopControllerBase>> dictionary)
        {
            dictionary ??= new();
            dictionary.Add(TeamType.Ally, new List<TroopControllerBase>());
            dictionary.Add(TeamType.Enemy, new List<TroopControllerBase>());
        }

        private void CreateAllTroops()
        {
            for (int i = 0; i < allAllyTroops.Value.Count; i++)
            {
                var ally = allAllyTroops.Value[i].Create(unseenTransform);
                ally.gameObject.SetActive(false);
                createdTroops[TeamType.Ally].Add(ally);
            }

            for (int i = 0; i < enemyTroops.Value.Count; i++)
            {
                var enemy = enemyTroops.Value[i].Create(unseenTransform);
                enemy.gameObject.SetActive(false);
                createdTroops[TeamType.Enemy].Add(enemy);
            }
        }

        #endregion

        #region OnBattleStart

        public void Setup()
        {
            SetTroopsReady();
        }

        private void SetTroopsReady()
        {
            var matchedControllers = createdTroops[TeamType.Ally]
                .Where(controller => allyTroops.Value.Contains(controller.data)).ToList();
            aliveTroops[TeamType.Enemy].AddRange(createdTroops[TeamType.Enemy]);
            aliveTroops[TeamType.Ally].AddRange(matchedControllers);
            SetTroopBehaviours(AllyPositions, TeamType.Ally);
            SetTroopBehaviours(EnemyPositions, TeamType.Enemy);
        }

        private void SetTroopBehaviours(List<Transform> spawnPositions, TeamType teamType)
        {
            for (var i = 0; i < aliveTroops[teamType].Count; i++)
            {
                aliveTroops[teamType][i].gameObject.SetActive(true);
                aliveTroops[teamType][i].SetTroopReady(spawnPositions[i]);
                aliveTroops[teamType][i].AssignTargetTroops(aliveTroops[GetOtherTeam(teamType)]);
                aliveTroops[teamType][i].TroopDiedEvent += DeActivateTroop;
            }
        }

        #endregion

        private void DeActivateTroop(TroopControllerBase deadTroop)
        {
            TeamType teamType = deadTroop.data.teamType;
            if (aliveTroops[teamType].Contains(deadTroop))
            {
                RemoveTroop(deadTroop);
                CheckRemainingTeams(teamType, aliveTroops[teamType].Count);
            }
        }

        private void RemoveTroop(TroopControllerBase deadTroop)
        {
            deadTroop.TroopDiedEvent -= DeActivateTroop;
            RemoveTroopFromTargets(deadTroop);
            if (aliveTroops[deadTroop.data.teamType].Contains(deadTroop))
                aliveTroops[deadTroop.data.teamType].Remove(deadTroop);
            deadTroop.ResetTroop(unseenTransform);
            deadTroop.gameObject.SetActive(false);
        }

        private void RemoveTroopFromTargets(TroopControllerBase deadTroop)
        {
            var otherTeam = GetOtherTeam(deadTroop.data.teamType);
            foreach (var troop in aliveTroops[otherTeam])
            {
                troop.RemoveTargetTroop(deadTroop);
            }
        }

        public void RemoveAllAliveTroops()
        {
            Remove(TeamType.Ally);
            Remove(TeamType.Enemy);
        }

        private void Remove(TeamType teamType)
        {
            if (!aliveTroops.ContainsKey(teamType)) return;
            for (var i = 0; i < aliveTroops[teamType].Count; i++)
            {
                RemoveTroop(aliveTroops[teamType][i]);
            }
        }

        public TroopControllerBase GetRandomEnemy()
        {
            var aliveEnemyTroops = aliveTroops[TeamType.Enemy];
            var randomEnemy = aliveEnemyTroops[Random.Range(0, aliveEnemyTroops.Count)];
            return randomEnemy;
        }

        private void CheckRemainingTeams(TeamType teamType, int count)
        {
            if (teamType == TeamType.Ally)
            {
                OnAllyTroopEliminated?.Invoke(count);
            }
            else
            {
                OnEnemyTroopEliminated?.Invoke(count);
            }
        }

        private static TeamType GetOtherTeam(TeamType teamType)
        {
            return teamType == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
        }
    }
}