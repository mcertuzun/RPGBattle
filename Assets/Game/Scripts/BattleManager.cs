using System.Collections.Generic;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Teams")] 
        public List<TroopController> allyTroops;
        public List<TroopController> enemyTroops;
        public Dictionary<TeamType, List<TroopController>> aliveTroops;

        [Header("Win Battle")] public SceneTimelineBehaviour victoryCutsceneBehaviour;
        [Header("Lose Battle")] public SceneTimelineBehaviour defeatCutsceneBehaviour;

        public void SetupTroops()
        {
            if (aliveTroops == null)
                aliveTroops = new Dictionary<TeamType, List<TroopController>>();

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
                aliveTroops[teamType][i].UnitDiedEvent += UnitHasDied;
            }
        }

        private void UnitHasDied(TroopController deadTroop)
        {
            RemoveUnitFromAliveUnits(deadTroop);
        }

        private void RemoveUnitFromAliveUnits(TroopController deadTroop)
        {
            CheckRemainingTeams();

            foreach (var troop in aliveTroops[deadTroop.data.teamType])
            {
                if (troop == deadTroop)
                {
                    troop.UnitDiedEvent -= UnitHasDied;
                    aliveTroops[deadTroop.data.teamType].Remove(troop);
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
            //Victory UILog.
            Debug.Log("Victory");
        }

        private void SetBattleDefeat()
        {
            //Defeat UI
            Debug.Log("Defeat");
        }
    }
}