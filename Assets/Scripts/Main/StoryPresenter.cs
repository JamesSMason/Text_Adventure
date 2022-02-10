using Adventure.Core;
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

        StoryNode currentStoryNode = null;
        StoryTracker storyTracker = null;

        public Action OnStoryUpdate;

        void Awake()
        {
            storyTracker = GetComponent<StoryTracker>();
        }

        void Start()
        {
            if (currentStoryNode == null)
            {
                MoveToNextNode(currentStory.GetRootNode().name);
            }
        }

        public string GetStoryText()
        {
            return currentStoryNode.GetStoryText();
        }

        public IEnumerable<string> GetAllChildrenIDs()
        {
            foreach (ChildNode child in FilterOnCondition(currentStoryNode.GetChildNodes()))
            {
                yield return child.GetChildID();
            }
        }

        public string GetOptionText(string childID)
        {
            return currentStoryNode.GetOption(childID);
        }

        public void MoveToNextNode(string childID)
        {
            TriggerExitAction();
            currentStoryNode = currentStory.GetStoryNode(childID);
            storyTracker.AddNode(currentStoryNode);

            TriggerEnterAction();

            OnStoryUpdate();
        }

        public bool GetIsEncounter()
        {
            return currentStoryNode.GetEncounter() != null;
        }

        public void AdjustStats()
        {
            PlayerPresenter playerPresenter = FindObjectOfType<PlayerPresenter>();
            for (int i = 0; i < currentStoryNode.GetStatsCount(); i++)
            {
                playerPresenter.AdjustStat(currentStoryNode.GetStatsToAdjust(i), currentStoryNode.GetAdjustmentValue(i));
                Debug.Log($"Adjust {currentStoryNode.GetStatsToAdjust(i)} by {currentStoryNode.GetAdjustmentValue(i)}");
            }
        }

        private IEnumerable<ChildNode> FilterOnCondition(IEnumerable<ChildNode> inputNode)
        {
            foreach (ChildNode node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if (currentStoryNode != null)
            {
                TriggerAction(currentStoryNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentStoryNode != null)
            {
                TriggerAction(currentStoryNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") {  return; }
            foreach (StoryTrigger trigger in GetComponents<StoryTrigger>())
            {
                trigger.Trigger(action);
            }
        }

        public object CaptureState()
        {
            return currentStoryNode.name;
        }

        public void RestoreState(object state)
        {
            MoveToNextNode((string)state);
            OnStoryUpdate();
        }
    }
}