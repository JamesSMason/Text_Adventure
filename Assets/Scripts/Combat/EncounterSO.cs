using System;
using UnityEngine;

namespace Adventure.Combat
{
    [CreateAssetMenu(fileName = "New Encounter", menuName = "Adventure/New Encounter", order = 1)]
    public class EncounterSO : ScriptableObject
    {
        [SerializeField] Monster[] monsters;
        [SerializeField] bool canEscape = false;
        [SerializeField] int escapeRound = 0;

        public Monster[] GetMonsters() { return monsters; }

        public bool GetCanEscape { get { return canEscape; } }

        public int GetEscapeRound { get { return escapeRound; } }
    }

    [Serializable]
    public class Monster
    {
        [SerializeField] string monsterName = "New name";
        [SerializeField] int monsterSkill = 6;
        [SerializeField] int monsterStamina = 6;
        [SerializeField] Sprite monsterImage = null;

        public string GetMonsterName() {  return monsterName; }
        public int GetMonsterSkill() { return monsterSkill; }
        public int GetMonsterStamina() { return monsterStamina; }
        public Sprite GetMonsterImage() { return monsterImage; }
    }
}