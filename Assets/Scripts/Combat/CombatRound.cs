using Adventure.Attributes;
using Adventure.Core;
using Adventure.Main;

namespace Adventure.Combat
{
    public class CombatRound
    {
        int numberOfDice = 2;

        public CombatRound(PlayerPresenter player, Encounter currentEncounter, DiceRoller diceRoller)
        {
            currentEncounter.IncrementRoundNumber();
            currentEncounter.SetPlayerWonRound(null);
            int playerDiceRoll = diceRoller.GenerateDiceRollResult(numberOfDice);
            int creatureDiceRoll = diceRoller.GenerateDiceRollResult(numberOfDice);
            int PAS = playerDiceRoll + player.GetStat(Stats.Skill);
            int CAS = creatureDiceRoll + currentEncounter.GetPlayerTarget().GetMonsterSkill();

            if (PAS > CAS)
            {
                currentEncounter.SetPlayerWonRound(true);
                currentEncounter.ApplyDamageToMonster(currentEncounter.GetDefaultDamage());
            }
            else if (CAS > PAS)
            {
                currentEncounter.SetPlayerWonRound(false);
                player.AdjustStat(Stats.Stamina, -currentEncounter.GetDefaultDamage());
            }
        }
    }
}