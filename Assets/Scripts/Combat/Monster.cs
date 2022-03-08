using System;
using UnityEngine;

namespace Adventure.Combat
{
    [Serializable]
    public class Monster
    {
        [SerializeField] string monsterName = "New name";
        [SerializeField] int monsterSkill = 6;
        [SerializeField] int monsterStamina = 6;
        [SerializeField] Sprite monsterImage = null;

        public string GetMonsterName() { return monsterName; }
        public int GetMonsterSkill() { return monsterSkill; }
        public int GetMonsterStamina() { return monsterStamina; }
        public Sprite GetMonsterImage() { return monsterImage; }
    }
}