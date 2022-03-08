using UnityEngine;

public class CombatMessages : MonoBehaviour
{
    [SerializeField] string playerHitSuccess = "You hit the monster!\n";
    [SerializeField] string monsterHitSuccess = "The monster hit you!\n";
    [SerializeField] string noHitSuccess = "You test each others defences.\n";

    string messageUpdate = "";

    // Getter
    public string GetMessageUpdate()
    {
        return messageUpdate;
    }

    // Setter
    public void ResetMessageUpdate()
    {
        messageUpdate = "";
    }

    // Messages
    public void ReportRoundNumber(int roundNumber)
    {
        messageUpdate += $"\nRound {roundNumber}:\n";
    }

    public void ReportRollResult(int playerRoll, int PAS, int monsterRoll, int CAS)
    {
        messageUpdate += $"You rolled {playerRoll} for a total of {PAS}.\n";
        messageUpdate += $"The monster rolled {monsterRoll} for a total of {CAS}.\n";
    }

    public void ReportRoundResult(bool? playerWonRound)
    {
        if (playerWonRound == true)
        {
            messageUpdate += playerHitSuccess;
        }
        else if (playerWonRound == false)
        {
            messageUpdate += monsterHitSuccess;
        }
        else
        {
            messageUpdate += noHitSuccess;
        }
    }

    public void ReportMonsterDead()
    {
        messageUpdate += "The monster falls to the ground...dead!\n";
    }
}