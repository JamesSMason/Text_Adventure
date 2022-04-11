using Adventure.Attributes;
using System;
using UnityEngine;

namespace Adventure.Main
{
    public class PlayerPresenter : MonoBehaviour
    {
        Player player;
        StatTests statTest;

        public Action OnStatisticChange;

        void Awake()
        {
            player = GetComponent<Player>();
            statTest = FindObjectOfType<StatTests>();
        }

        private void OnEnable()
        {
            if (player != null)
            {
                player.OnStatisticsChange += RefreshStats;
            }
        }

        private void OnDisable()
        {
            if (player != null)
            {
                player.OnStatisticsChange -= RefreshStats;
            }
        }

        private void RefreshStats()
        {
            OnStatisticChange();
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
            RefreshStats();
        }

        public bool TestStat(Stats stat, bool decrementStat)
        {
            bool hasPassed = statTest.TestStat(player, stat, decrementStat);
            if (decrementStat)
            {
                RefreshStats();
            }
            return hasPassed;
        }

        public bool TestStat(Stats stat1, Stats stat2)
        {
            bool hasPassed = statTest.TestStat(player, stat1, stat2);
            return hasPassed;
        }
    }
}