using Adventure.Attributes;
using Adventure.Main;

namespace Adventure.Combat
{
    public class CombatLuck
    {
        public CombatLuck(PlayerPresenter player, Encounter encounter, CombatMessages messages)
        {
            bool? playerWonRound = encounter.GetHasPlayerWonRound();
            if (playerWonRound == null) { return; }
            bool hasPassed = player.TestStat(Stats.Luck, true);
            
            if (playerWonRound == true)
            {
                if (hasPassed)
                {
                    encounter.ApplyDamageToMonster(-2);
                }
                else
                {
                    encounter.ApplyDamageToMonster(1);
                }
            }
            else if (encounter.GetHasPlayerWonRound() == false)
            {
                if (hasPassed)
                {
                    player.AdjustStat(Stats.Stamina, 1);
                }
                else
                {
                    player.AdjustStat(Stats.Stamina, -1);
                }
            }
            messages.ReportLuckResult(hasPassed, playerWonRound);
        }
    }
}