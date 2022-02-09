using Adventure.Core;
using Adventure.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    [Serializable]
    public class StoryTracker : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        Dictionary<string, bool> storyTracker = new Dictionary<string, bool>();

        public void AddNode(StoryNode node)
        {
            storyTracker[node.name] = true;
        }

        public bool CheckForNode(string childID)
        {
            return storyTracker.ContainsKey(childID);
        }

        public object CaptureState()
        {
            return storyTracker;
        }

        public void RestoreState(object state)
        {
            storyTracker = (Dictionary<string, bool>)state;
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasVisited":
                return CheckForNode(parameters[0]);
            }    

            return null;
        }
    }
}