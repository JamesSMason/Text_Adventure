using System;
using UnityEngine;

namespace Adventure.Core
{
    [CreateAssetMenu(fileName = "New Encounter", menuName = "Adventure/New Encounter", order = 1)]
    public class EncounterSO : ScriptableObject
    {
        [SerializeField] Monster[] monsters;

        public Monster[] GetMonsters() { return monsters; }
    }

    [Serializable]
    public class Monster
    {
        [SerializeField] string monsterName = "New name";
        [SerializeField] int monsterSkill = 6;
        [SerializeField] int monsterStamina = 6;

        public string GetMonsterName() {  return monsterName; }
        public int GetMonsterSkill() { return monsterSkill; }
        public int GetMonsterStamina() { return monsterStamina; }
    }
}