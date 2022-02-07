using UnityEngine;

namespace Adventure.Core
{
    public class DiceRoller : MonoBehaviour
    {
        const int NUMBER_OF_SIDES_ON_DIE = 6;

        public int GenerateDiceRollResult(int numberOfDice)
        {
            int result = 0;
            for (int rollNumber = 0; rollNumber < numberOfDice; rollNumber++)
            {
                result += Random.Range(1, NUMBER_OF_SIDES_ON_DIE + 1);
            }
            return result;
        }
    }
}