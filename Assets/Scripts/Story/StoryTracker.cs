using Adventure.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    [Serializable]
    public class StoryTracker : MonoBehaviour, ISaveable
    {
        Dictionary<string, bool> storyTracker = new Dictionary<string, bool>();

        public void AddNode(StoryNode node)
        {
            storyTracker[node.name] = true;
        }

        public bool CheckForNode(StoryNode node)
        {
            return storyTracker.ContainsKey(node.name);
        }

        public object CaptureState()
        {
            return storyTracker;
        }

        public void RestoreState(object state)
        {
            storyTracker = (Dictionary<string, bool>)state;
        }
    }
}