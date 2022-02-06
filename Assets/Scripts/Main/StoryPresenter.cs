using Adventure.Story;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Main
{
    public class StoryPresenter : MonoBehaviour
    {
        [SerializeField] StorySO currentStory = null;

        public Action OnStoryUpdate;

        StoryNode currentStoryNode = null;
        void Start()
        {
            currentStoryNode = currentStory.GetRootNode();
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
            OnStoryUpdate();
        }
    }
}