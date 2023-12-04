using System;
using System.Collections.Generic;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class TeamController : MonoBehaviour
    {
        private Dictionary<TeamType, List<TroopControllerBase>> aliveTroops;
        [Header("Data")] [SerializeField] private TroopDataList allyTroops, enemyTroops;
        [SerializeField] private List<Transform> AllyPositions;
        [SerializeField] private List<Transform> EnemyPositions;
        public event Action<int> OnAllyTroopEliminated;
        public event Action<int> OnEnemyTroopEliminated;

        public void Setup()
        {
            CreateAliveTroops();
            SetAllTeamsTargetTroops();
        }

        public TroopControllerBase GetRandomEnemy()
        {
            var aliveEnemyTroops = aliveTroops[TeamType.Enemy];
            var randomEnemy = aliveEnemyTroops[Random.Range(0, aliveEnemyTroops.Count)];
            return randomEnemy;
        }
        
        private void CreateAliveTroops()
        {
            InitDictionary();
            CreateAllTroops();
            SetTroops();
        }

        private void InitDictionary()
        {
            aliveTroops ??= new();
            aliveTroops.Add(TeamType.Ally, new List<TroopControllerBase>());
            aliveTroops.Add(TeamType.Enemy, new List<TroopControllerBase>());
        }

        private void CreateAllTroops()
        {
            CreateTroops(allyTroops, AllyPositions);
            CreateTroops(enemyTroops, EnemyPositions);
        }

        private void CreateTroops(TroopDataList dataList, List<Transform> spawnPositions)
        {
            for (var i = 0; i < dataList.Value.Count; i++)
            {
                var createdTroop = dataList.Value[i].Create(spawnPositions[i]);
                aliveTroops[dataList.Value[i].teamType].Add(createdTroop);
            }
        }

        private void SetTroops()
        {
            foreach (var teamType in aliveTroops.Keys)
            {
                for (int i = 0; i < aliveTroops[teamType].Count; i++)
                {
                    aliveTroops[teamType][i].SetHealth();
                    aliveTroops[teamType][i].TroopDiedEvent += RemoveTroop;
                }
            }
        }

        private void SetAllTeamsTargetTroops()
        {
            foreach (var teamType in aliveTroops.Keys)
            {
                SetTargets(teamType);
            }
        }

        private void SetTargets(TeamType teamType)
        {
            for (int i = 0; i < aliveTroops[teamType].Count; i++)
            {
                aliveTroops[teamType][i].AssignTargetTroops(aliveTroops[GetOtherTeam(teamType)]);
            }
        }

        private void RemoveTroopFromTargets(TroopControllerBase deadTroop)
        {
            var otherTeam = GetOtherTeam(deadTroop.data.teamType);
            foreach (var troop in aliveTroops[otherTeam])
            {
                troop.RemoveTargetTroop(deadTroop);
            }
        }

        
        //TODO: DON'T REMOVE JUST RESET! 
        private void RemoveTroop(TroopControllerBase deadTroop)
        {
            TeamType teamType = deadTroop.data.teamType;
            if (aliveTroops[teamType].Contains(deadTroop))
            {
                KillTroop(deadTroop);
                CheckRemainingTeams(teamType, aliveTroops[teamType].Count);
            }
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
        private void KillTroop(TroopControllerBase deadTroop)
        {
            deadTroop.TroopDiedEvent -= RemoveTroop;
            RemoveTroopFromTargets(deadTroop);
            aliveTroops[deadTroop.data.teamType].Remove(deadTroop);
            Destroy(deadTroop.gameObject);
        }

        public void RemoveAllAliveTroops()
        {
            if (!aliveTroops.ContainsKey(TeamType.Ally)) return;
            for (var i = 0; i < aliveTroops[TeamType.Ally].Count; i++)
            {
                KillTroop(aliveTroops[TeamType.Ally][i]);
            }  
            if (!aliveTroops.ContainsKey(TeamType.Enemy)) return;
            for (var i = 0; i < aliveTroops[TeamType.Enemy].Count; i++)
            {
                KillTroop(aliveTroops[TeamType.Enemy][i]);
            }
            
            aliveTroops.Clear();
        }

        private static TeamType GetOtherTeam(TeamType teamType)
        {
            return teamType == TeamType.Ally ? TeamType.Enemy : TeamType.Ally;
        }
        
    }
}