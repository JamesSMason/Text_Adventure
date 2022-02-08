using Adventure.Saving;
using Adventure.Story;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Main
{
    public class StoryPresenter : MonoBehaviour, ISaveable
    {
        [SerializeField] StorySO currentStory = null;

        public Action OnStoryUpdate;

        StoryNode currentStoryNode = null;
        StoryTracker storyTracker = null;

        void Awake()
        {
            storyTracker = GetComponent<StoryTracker>();
        }

        void Start()
        {
            currentStoryNode = currentStory.GetRootNode();
            storyTracker.AddNode(currentStoryNode);
            OnStoryUpdate();
        }

        public string GetStoryText()
        {
            return currentStoryNode.GetStoryText();
        }

        public List<string> GetAllChildren()
        {
            return currentStoryNode.GetChildren();
        }

        public string GetOptionText(string childID)
        {
            return currentStoryNode.GetOption(childID);
        }

        public void MoveToNextNode(string childID)
        {
            currentStoryNode = currentStory.GetStoryNode(childID);
            storyTracker.AddNode(currentStoryNode);
            OnStoryUpdate();
        }

        public bool GetIsEncounter()
        {
            return currentStoryNode.GetEncounter() != null;
        }

        public object CaptureState()
        {
            return currentStoryNode.name;
        }

        public void RestoreState(object state)
        {
            currentStoryNode = currentStory.GetStoryNode((string)state);
            OnStoryUpdate();
        }
    }
}