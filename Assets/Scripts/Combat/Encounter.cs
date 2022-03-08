using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Combat
{
    public class Opponent
    {
        string monsterName = "New name";
        int monsterSkill = 6;
        int monsterStamina = 6;
        Sprite monsterImage = null;

        public Opponent(Monster monster)
        {
            monsterName = monster.GetMonsterName();
            monsterSkill = monster.GetMonsterSkill();
            monsterStamina = monster.GetMonsterStamina();
            monsterImage = monster.GetMonsterImage();
        }

        public string GetMonsterName() { return monsterName; }
        public int GetMonsterSkill() { return monsterSkill; }
        public int GetMonsterStamina() { return monsterStamina; }
        public Sprite GetMonsterImage() { return monsterImage; }

        public void ApplyDamage(int damage)
        {
            monsterStamina -= damage;
        }
    }

    public class Encounter
    {
        // Monsters
        Dictionary<int, Opponent> encounterLookup = new Dictionary<int, Opponent>();
        // Escape
        bool canEscape = false;
        int escapeRound = 0;
        // Round details
        int roundNumber = 0;
        int playersTargetIndex = 0;
        bool? hasPlayerWonRound = null;
        // Damage
        int defaultDamage = 2;

        // Encounter constructor
        public Encounter(EncounterSO encounterSO)
        {
            encounterLookup.Clear();
            Monster[] monsters = encounterSO.GetMonsters();
            for (int i = 0; i < monsters.Length; i++)
            {
                encounterLookup[i] = new Opponent(monsters[i]);
            }
            if (encounterSO.GetCanEscape)
            {
                canEscape = true;
                escapeRound = encounterSO.GetEscapeRound;
            }
        }

        // Getters
        public IEnumerable<Opponent> GetMonsters()
        {
            foreach (int index in encounterLookup.Keys)
            {
                yield return encounterLookup[index];
            }
        }

        public int GetMonsterCount()
        {
            return encounterLookup.Count;
        }

        public bool GetCanEscape()
        {
            return canEscape && roundNumber >= escapeRound;
        }

        public int GetRoundNumber()
        {
            return roundNumber;
        }

        public int GetPlayerTarget()
        {
            return playersTargetIndex;
        }

        public bool? GetHasPlayerWonRound()
        {
            return hasPlayerWonRound;
        }

        public int GetDefaultDamage()
        {
            return defaultDamage;
        }

        public bool GetIsSimultaneous()
        {
            return false;
        }

        // Setters
        public void IncrementRoundNumber()
        {
            roundNumber++;
        }

        public void SetPlayerTarget(int index)
        {
            playersTargetIndex = index;
        }

        public void SetPlayerWonRound(bool? playerWon)
        {
            hasPlayerWonRound = playerWon;
        }

        public void SetDefaultDamage(int damage)
        {
            defaultDamage = damage;
        }

        public void ApplyDamageToMonster(int damage)
        {
            encounterLookup[playersTargetIndex].ApplyDamage(damage);
            if (encounterLookup[playersTargetIndex].GetMonsterStamina() <= 0)
            {
                encounterLookup.Remove(playersTargetIndex);
                List<Opponent> tempList = new List<Opponent>();
                foreach (KeyValuePair<int, Opponent> pair in encounterLookup)
                {
                    if (encounterLookup[pair.Key] != null)
                    {
                        tempList.Add(pair.Value);
                    }
                }
                encounterLookup.Clear();
                for (int i = 0; i < tempList.Count; i++)
                {
                    encounterLookup[i] = tempList[i];
                }
            }
        }
    }
}