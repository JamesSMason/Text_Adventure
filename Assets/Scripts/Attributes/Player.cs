using Adventure.Core;
using Adventure.Saving;
using Adventure.SceneLoader;
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
        Dictionary<Stats, int> playerStats = null;

        public Action OnStatisticsChange;

        void Awake()
        {
            diceRoller = FindObjectOfType<DiceRoller>();
        }

        void Start()
        {
            if (playerStats == null)
            {
                playerStats = new Dictionary<Stats, int>();
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
            }
            OnStatisticsChange();
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

        private void SetStat(Stats stat, int value)
        {
            playerStats[stat] = value;
        }

        private void CheckForLoseCondition(Stats stat)
        {
            if (playerStats[stat] > 0) { return; }
            Debug.Log("You lose!");
            LoadScene sceneLoader = FindObjectOfType<LoadScene>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadGameOverScene();
            }
        }

        public int GetStat(Stats stat)
        {
            if (!playerStats.ContainsKey(stat)) { return -1; }
            return playerStats[stat];
        }

        public void AdjustStat(Stats stat, int adjustmentValue)
        {
            if (!playerStats.ContainsKey(stat))
            {
                SetStat(stat, adjustmentValue);
                return;
            }

            int newValue = 0;

            switch (stat)
            {
                case Stats.Skill:
                    newValue = MaximiseStat(Stats.InitialSkill, adjustmentValue);
                    break;
                case Stats.Stamina:
                    newValue = MaximiseStat(Stats.InitialStamina, adjustmentValue);
                    break;
                case Stats.Luck:
                    newValue = MaximiseStat(Stats.InitialLuck, adjustmentValue);
                    break;
                default:
                    newValue = playerStats[stat] + adjustmentValue;
                    break;
            }

            if (newValue < 0) { newValue = 0; }

            playerStats[stat] = newValue;

            if (stat == Stats.Skill || stat == Stats.Stamina || stat == Stats.Luck)
            {
                CheckForLoseCondition(stat);
            }
        }

        private int MaximiseStat(Stats stat, int adjustmentValue)
        {
            int newValue = playerStats[stat] + adjustmentValue;
            if (newValue > playerStats[stat])
            {
                newValue = playerStats[stat];
            }
            return newValue;
        }

        public object CaptureState()
        {
            return playerStats;
        }

        public void RestoreState(object state)
        {
            if (playerStats != null)
            {
                playerStats.Clear();
            }
            playerStats = (Dictionary<Stats, int>)state;
            OnStatisticsChange();
        }
    }
}