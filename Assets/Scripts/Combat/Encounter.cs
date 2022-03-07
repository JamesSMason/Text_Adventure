using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Combat
{
    public class Encounter
    {
        Dictionary<int, Monster> encounterLookup = new Dictionary<int, Monster>();
        bool canEscape = false;
        int escapeRound = 0;

        public Encounter(EncounterSO encounterSO)
        {
            encounterLookup.Clear();
            Monster[] monsters = encounterSO.GetMonsters();
            for (int i = 0; i < monsters.Length; i++)
            {
                encounterLookup[i] = monsters[i];
            }
            if (encounterSO.GetCanEscape)
            {
                canEscape = true;
                escapeRound = encounterSO.GetEscapeRound;
            }
        }

        public IEnumerable<Monster> GetMonsters()
        {
            foreach (int index in encounterLookup.Keys)
            {
                yield return encounterLookup[index];
            }
        }
    }
}