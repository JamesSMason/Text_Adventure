using Adventure.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    [Serializable]
    public class ChildNode
    {
        [SerializeField] string childID;
        [SerializeField] string optionText;
        [SerializeField] Condition condition;

        public ChildNode(string childID, string optionText)
        {
            this.childID = childID;
            this.optionText = optionText;
        }

        public string GetChildID() { return childID; }
        public string GetOptionText() { return optionText; }

        public void SetOptionText(string optionText) { this.optionText = optionText; }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }
    }
}