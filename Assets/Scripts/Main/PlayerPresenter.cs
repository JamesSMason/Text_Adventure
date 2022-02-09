using Adventure.Attributes;
using System;
using UnityEngine;

namespace Adventure.Main
{
    public class PlayerPresenter : MonoBehaviour
    {
        Player player;

        public Action OnStatisticChange;

        void Awake()
        {
            player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            if (player != null)
            {
                player.OnStatisticLoad += RefreshStats;
            }
        }

        private void OnDisable()
        {
            if (player != null)
            {
                player.OnStatisticLoad -= RefreshStats;
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
    }
}