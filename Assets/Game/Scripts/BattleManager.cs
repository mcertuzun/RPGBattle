using System.Collections.Generic;
using Game.Scripts.Controllers.Troop;
using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Behaviours
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Teams")] 
        [SerializeField] private List<TroopControllerBase> allyTroops;
        [SerializeField] private List<TroopControllerBase> enemyTroops;
        private Dictionary<TeamType, List<TroopControllerBase>> aliveTroops;
        public TeamTypeBehaviour teamTypeBehaviour;

        // [Header("Win Battle")] public SceneTimelineBehaviour victoryCutsceneBehaviour;
        // [Header("Lose Battle")] public SceneTimelineBehaviour defeatCutsceneBehaviour;
        
        private void Awake()
        {
            SetupTeams();
        }

        private void SetupTeams()
        {
            CreateAliveTroops();
            AutoAssignTroopTeamTargets();
        }

        private void TeamTypeChangedEvent(TeamType teamtype)
        {
            if (teamtype == TeamType.Enemy)
            {
                var randomEnemy = aliveTroops[teamtype][Random.Range(0, aliveTroops[teamtype].Count)];
                randomEnemy.StartBattle();
            }
        }

        private void CreateAliveTroops()
        {
            aliveTroops ??= new();
            aliveTroops.Add(TeamType.Ally, new List<TroopControllerBase>());
            aliveTroops[TeamType.Ally].AddRange(allyTroops);
            aliveTroops.Add(TeamType.Enemy, new List<TroopControllerBase>());
            aliveTroops[TeamType.Enemy].AddRange(enemyTroops);

            SetTroops(TeamType.Ally);
            SetTroops(TeamType.Enemy);
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

            for (var i = 0; i < aliveTroops[deadTroop.data.teamType].Count; i++)
            {
                var troop = aliveTroops[deadTroop.data.teamType][i];
                if (troop == deadTroop)
                {
                    troop.TroopDiedEvent -= TroopHasDied;
                    aliveTroops[troop.data.teamType].Remove(deadTroop);
                    RemoveTroopFromTargets(deadTroop);
                }
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
            foreach (var troop in aliveTroops[deadTroop.data.teamType])
            {
                if (troop == deadTroop)
                {
                    troop.RemoveTargetUnit(deadTroop);
                }
            }
        }

        private void StopAllAliveTeamTroops(List<TroopControllerBase> aliveTeamTroops)
        {
            for (int i = 0; i < aliveTeamTroops.Count; i++)
            {
                // aliveTeamTroops[i].BattleEnded();
            }
        }
        private void OnEnable() => teamTypeBehaviour.TeamTypeChangedEvent += TeamTypeChangedEvent;
        private void OnDisable() => teamTypeBehaviour.TeamTypeChangedEvent -= TeamTypeChangedEvent;
    }
}