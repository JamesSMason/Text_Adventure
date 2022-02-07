using Adventure.Core;
using Adventure.Attributes;
using System;
using UnityEngine;

namespace Adventure.Main
{
    public class PlayerPresenter : MonoBehaviour
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
        Player player;
        DiceRoller diceRoller = null;

        public Action OnStatisticChange;

        void Awake()
        {
            diceRoller = FindObjectOfType<DiceRoller>();
        }

        void Start()
        {
            CreatePlayer();
        }

        public int GetStat(Stats stat)
        {
            return player.GetStat(stat);
        }

        public string GetStatAsString(Stats stat)
        {
            return player.GetStat(stat).ToString();
        }

        public void AdjustStat(Stats stat, int value)
        {
            player.AdjustStat(stat, value);
        }

        private void CreatePlayer()
        {
            if (useTestStats)
            {
                player = new Player(testSkillValue, testStaminaValue, testLuckValue,
                                        initialGold, initialProvisions, initialJewellery);
            }
            else
            {
                int skill = diceRoller.GenerateDiceRollResult(1) + 6;
                int stamina = diceRoller.GenerateDiceRollResult(2) + 12;
                int luck = diceRoller.GenerateDiceRollResult(1) + 6;
                player = new Player(skill, stamina, luck, initialGold, initialProvisions, initialJewellery);
            }
            OnStatisticChange();
        }
    }
}