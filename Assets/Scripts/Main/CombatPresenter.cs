using Adventure.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Main
{
    public class CombatPresenter : MonoBehaviour
    {
        Encounter currentEncounter = null;

        public Action OnCombatUpdate;

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

        public void FightOn()
        {
            currentEncounter.ApplyDamageToMonster(currentEncounter.GetDefaultDamage());
            OnCombatUpdate();
        }

        public void Escape()
        {
            Debug.Log("Escape");
        }

        public void Death()
        {
            Debug.Log("You dead");
        }

        public void Victory()
        {
            Debug.Log("You won!");
        }

        public void TestYourLuck()
        {
            Debug.Log("You feeling lucky, punk?");
        }

        public void SetTarget()
        {
            Debug.Log("Stay on target!");
        }
    }
}