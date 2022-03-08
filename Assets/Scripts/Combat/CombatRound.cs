using Adventure.Attributes;
using Adventure.Core;
using Adventure.Main;

namespace Adventure.Combat
{
    public class CombatRound
    {
        int numberOfDice = 2;

        public CombatRound(PlayerPresenter player, Encounter currentEncounter, DiceRoller diceRoller, CombatMessages combatMessages)
        {
            currentEncounter.IncrementRoundNumber();
            currentEncounter.SetPlayerWonRound(null);
            combatMessages.ResetMessageUpdate();
            int playerDiceRoll = diceRoller.GenerateDiceRollResult(numberOfDice);
            int creatureDiceRoll = diceRoller.GenerateDiceRollResult(numberOfDice);
            int PAS = playerDiceRoll + player.GetStat(Stats.Skill);
            int CAS = creatureDiceRoll + currentEncounter.GetPlayerTarget().GetMonsterSkill();

            combatMessages.ReportRoundNumber(currentEncounter.GetRoundNumber());
            combatMessages.ReportRollResult(playerDiceRoll, PAS, creatureDiceRoll, CAS);

            if (PAS > CAS)
            {
                currentEncounter.SetPlayerWonRound(true);
                combatMessages.ReportRoundResult(true);
                bool monsterDead = currentEncounter.ApplyDamageToMonster(currentEncounter.GetDefaultDamage());
                if (monsterDead)
                {
                    combatMessages.ReportMonsterDead();
                }
            }
            else if (CAS > PAS)
            {
                currentEncounter.SetPlayerWonRound(false);
                combatMessages.ReportRoundResult(false);
                player.AdjustStat(Stats.Stamina, currentEncounter.GetDefaultDamage());
            }
            else
            {
                currentEncounter.SetPlayerWonRound(null);
                combatMessages.ReportRoundResult(null);
            }
        }
    }
}