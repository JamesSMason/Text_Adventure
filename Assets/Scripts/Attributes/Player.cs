using System.Collections.Generic;

namespace Adventure.Attributes
{
    public class Player
    {
        Dictionary<Stats, int> playerStats = new Dictionary<Stats, int>();

        public Player(int skill, int stamina, int luck,
                        int initialGold, int initialProvisions, int initialJewellery)
        {
            playerStats.Clear();
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

        public void SetStat(Stats stat, int value)
        {
            playerStats[stat] = value;
        }

        public void AdjustStat(Stats stat, int adjustmentValue)
        {
            if (playerStats.ContainsKey(stat))
            {
                playerStats[stat] += adjustmentValue;
            }
        }

        public void ClearStats()
        {
            playerStats.Clear();
        }
    }
}