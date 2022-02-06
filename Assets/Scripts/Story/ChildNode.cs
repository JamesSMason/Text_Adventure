using System;
using UnityEngine;

namespace Adventure.Story
{
    [Serializable]
    public class ChildNode
    {
        [SerializeField] string childID;
        [SerializeField] string optionText;

        public ChildNode(string childID, string optionText)
        {
            this.childID = childID;
            this.optionText = optionText;
        }

        public string GetChildID() { return childID; }
        public string GetOptionText() { return optionText; }

        public void SetOptionText(string optionText) { this.optionText = optionText; }
    }
}