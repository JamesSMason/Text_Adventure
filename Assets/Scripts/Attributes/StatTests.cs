using Adventure.Core;
using UnityEngine;

namespace Adventure.Attributes
{
    public class StatTests : MonoBehaviour
    {
        [SerializeField] int numberOfDiceUsedInTest = 2;

        public bool TestStat(Player player, Stats stat, bool decrementStat)
        {
            if (player == null) { return false; }
            int rollResult = GetComponent<DiceRoller>().GenerateDiceRollResult(numberOfDiceUsedInTest);
            bool hasPassedTest = (rollResult <= player.GetStat(stat));
            if (decrementStat)
            {
                player.AdjustStat(stat, -1);
            }
            return hasPassedTest;
        }

        public bool TestStat(Player player, Stats stat1, Stats stat2)
        {
            if (player == null) { return false; }
            int rollResult = GetComponent<DiceRoller>().GenerateDiceRollResult(numberOfDiceUsedInTest);
            return ((rollResult <= player.GetStat(stat1)) && (rollResult <= player.GetStat(stat2)));
        }
    }
}