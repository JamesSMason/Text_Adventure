using System.Collections.Generic;

namespace Adventure.Combat
{
    public class CombatOptions
    {
        public List<string> BuildOptions(Encounter currentEncounter)
        {
            List<string> options = new List<string>();
            if (currentEncounter.GetMonsterCount() == 0)
            {
                options.Add("Victory");
                return options;
            }
            if (currentEncounter.GetHasPlayerWonRound() != null)
            {
                options.Add("TYL");
                return options;
            }
            if (currentEncounter.GetIsSimultaneous())
            {
                options.Add("SetTarget");
                return options;
            }
            if (currentEncounter.GetCanEscape())
            {
                options.Add("Escape");
            }
            options.Add("FightOn");

            return options;
        }
    }

}