using Adventure.Core;
using Adventure.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Attributes
{
    public class Player : MonoBehaviour, ISaveable
    {
        [Header("---Initial Settings---")]
        [SerializeField] int initialGold = 0;
        [SerializeField] int initialProvisions = 10;
        [SerializeField] int initialJewellery = 0;
        [Header("---TEST VARIABLES---")]
        [SerializeField] bool useTestStats = false;
        [SerializeField] int testSkillValue = 1;
        [SerializeField] int testStaminaValue = 1;
        [SerializeField] int testLuckValue = 1;

        DiceRoller diceRoller = null;
        Dictionary<Stats, int> playerStats = new Dictionary<Stats, int>();

        public Action OnStatisticLoad;

        void Awake()
        {
            diceRoller = FindObjectOfType<DiceRoller>();
        }

        void Start()
        {
            if (useTestStats)
            {
                GenerateStats(testSkillValue, testStaminaValue, testLuckValue, initialGold, initialProvisions, initialJewellery);
            }
            else
            {
                int skill = diceRoller.GenerateDiceRollResult(1) + 6;
                int stamina = diceRoller.GenerateDiceRollResult(2) + 12;
                int luck = diceRoller.GenerateDiceRollResult(1) + 6;
                GenerateStats(skill, stamina, luck, initialGold, initialProvisions, initialJewellery);
            }
            OnStatisticLoad();
        }

        private void GenerateStats(int skill, int stamina, int luck,
                        int initialGold, int initialProvisions, int initialJewellery)
        {
            SetStat(Stats.InitialSkill, skill);
            SetStat(Stats.Skill, skill);
            SetStat(Stats.InitialStamina, stamina);
            SetStat(Stats.Stamina, stamina);
            SetStat(Stats.InitialLuck, luck);
            SetStat(Stats.Luck, luck);
            SetStat(Stats.Gold, initialGold);
            SetStat(Stats.Provisions, initialProvisions);
            SetStat(Stats.Jewellery, initialJewellery);
        }

        public int GetStat(Stats stat)
        {
            if (!playerStats.ContainsKey(stat)) { return -1; }
            return playerStats[stat];
        }

        public void AdjustStat(Stats stat, int adjustmentValue)
        {
            if (playerStats.ContainsKey(stat))
            {
                playerStats[stat] += adjustmentValue;
            }
        }

        public void SetStat(Stats stat, int value)
        {
            playerStats[stat] = value;
        }

        public object CaptureState()
        {
            return playerStats;
        }

        public void RestoreState(object state)
        {
            playerStats.Clear();
            playerStats = (Dictionary<Stats, int>)state;
            OnStatisticLoad();
        }
    }
}