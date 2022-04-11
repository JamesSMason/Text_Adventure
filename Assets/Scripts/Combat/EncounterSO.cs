using UnityEngine;

namespace Adventure.Combat
{
    [CreateAssetMenu(fileName = "New Encounter", menuName = "Adventure/New Encounter", order = 1)]
    public class EncounterSO : ScriptableObject
    {
        [SerializeField] Monster[] monsters;
        [SerializeField] bool canEscape = false;
        [SerializeField] int escapeRound = 0;
        [SerializeField] bool isRandom = false;

        public Monster[] GetMonsters() { return monsters; }

        public bool GetCanEscape { get { return canEscape; } }

        public int GetEscapeRound { get { return escapeRound; } }

        public bool GetIsRandom { get { return isRandom; } }
    }
}