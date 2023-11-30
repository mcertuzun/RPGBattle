using System.Collections.Generic;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Teams")] public List<TroopController> allyTroops;
        public List<TroopController> enemyTroops;
        public Dictionary<TeamType, List<TroopController>> aliveTroops;

        [Header("Win Battle")] public SceneTimelineBehaviour victoryCutsceneBehaviour;
        [Header("Lose Battle")] public SceneTimelineBehaviour defeatCutsceneBehaviour;

        private void Awake()
        {
            SetupTeams();
            StartBattle();
        }

        private void SetupTeams()
        {
            CreateAliveTroops();
            AutoAssignTroopTeamTargets();
        }

        private void StartBattle()
        {
            for (int i = 0; i < aliveTroops.Keys.Count; i++)
            {
                aliveTroops[TeamType.Ally][i].BattleStarted();
            }

            for (int i = 0; i < aliveTroops.Keys.Count; i++)
            {
                aliveTroops[TeamType.Enemy][i].BattleStarted();
            }
        }

        private void CreateAliveTroops()
        {
            aliveTroops ??= new Dictionary<TeamType, List<TroopController>>();
            aliveTroops.Add(TeamType.Ally, allyTroops);
            aliveTroops.Add(TeamType.Enemy, enemyTroops);
            SetTroops(TeamType.Ally);
            SetTroops(TeamType.Enemy);
        }

        private void SetTroops(TeamType teamType)
        {
            for (int i = 0; i < aliveTroops[teamType].Count; i++)
            {
                aliveTroops[teamType][i].SetTroopData();
                aliveTroops[teamType][i].UnitDiedEvent += TroopHasDied;
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

        private void TroopHasDied(TroopController deadTroop)
        {
            RemoveTroopFromAliveTroops(deadTroop);
        }

        private void RemoveTroopFromAliveTroops(TroopController deadTroop)
        {
            CheckRemainingTeams();

            for (var i = 0; i < aliveTroops[deadTroop.data.teamType].Count; i++)
            {
                var troop = aliveTroops[deadTroop.data.teamType][i];
                if (troop == deadTroop)
                {
                    troop.UnitDiedEvent -= TroopHasDied;
                    aliveTroops[troop.data.teamType].Remove(deadTroop);
                    RemoveTroopFromTargets(deadTroop);
                }
            }

            CheckRemainingTeams();
        }

        void RemoveTroopFromTargets(TroopController deadTroop)
        {
            foreach (var troop in aliveTroops[deadTroop.data.teamType])
            {
                if (troop == deadTroop)
                {
                    troop.RemoveTargetUnit(deadTroop);
                }
            }
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

        private void StopAllAliveTeamTroops(List<TroopController> aliveTeamTroops)
        {
            for(int i = 0; i < aliveTeamTroops.Count; i++)
            {
                aliveTeamTroops[i].BattleEnded();
            }
        }
    }
}