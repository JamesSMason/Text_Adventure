using Adventure.Combat;
using Adventure.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Main
{
    public class CombatPresenter : MonoBehaviour
    {
        Encounter currentEncounter = null;
        DiceRoller diceRoller = null;
        PlayerPresenter player = null;
        CombatMessages combatMessages = null;

        public Action OnCombatUpdate;
        public Action OnCombatReportUpdate;

        void Awake()
        {
            diceRoller = FindObjectOfType<DiceRoller>();
            player = FindObjectOfType<PlayerPresenter>();
            combatMessages = GetComponent<CombatMessages>();
        }

        public void SetEncounter(EncounterSO encounterSO)
        {
            currentEncounter = new Encounter(encounterSO);
            if (OnCombatUpdate != null)
            {
                OnCombatUpdate();
            }
        }

        public IEnumerable<Opponent> GetMonsters()
        {
            foreach (Opponent monster in currentEncounter.GetMonsters())
            {
                yield return monster;
            }
        }

        public List<string> GetOptions()
        {
            if (currentEncounter == null) { return null; }
            CombatOptions options = new CombatOptions();
            return options.BuildOptions(currentEncounter);
        }

        public string GetCombatMessages()
        {
            return combatMessages.GetMessageUpdate();
        }

        public void FightOn()
        {
            CombatRound newRound = new CombatRound(player, currentEncounter, diceRoller, combatMessages);
            OnCombatUpdate();
            OnCombatReportUpdate();
        }

        public void TestYourLuck()
        {
            Debug.Log("You feeling lucky, punk?");
            currentEncounter.SetPlayerWonRound(null);
            OnCombatUpdate();
            //OnCombatReportUpdate();
        }

        public void SetTarget()
        {
            Debug.Log("Stay on target!");
        }
    }
}