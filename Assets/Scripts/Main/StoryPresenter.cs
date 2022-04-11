using Adventure.Attributes;
using Adventure.Core;
using Adventure.Inventories;
using Adventure.Saving;
using Adventure.Story;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Main
{
    public class StoryPresenter : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        [SerializeField] StorySO currentStory = null;

        StoryNode currentStoryNode = null;
        StoryTracker storyTracker = null;

        CombatPresenter currentEncounter = null;

        bool hasPassedTest = false;

        public Action OnStoryUpdate;

        void Awake()
        {
            storyTracker = GetComponent<StoryTracker>();
            currentEncounter = FindObjectOfType<CombatPresenter>();
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
            hasPassedTest = false;

            TriggerEnterAction();

            OnStoryUpdate();

            if (GetIsEncounter())
            {
                currentEncounter.SetEncounter(currentStoryNode.GetEncounter());
            } 
        }

        public bool GetIsEncounter()
        {
            return currentStoryNode.GetEncounter() != null;
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
                foreach (var enterAction in currentStoryNode.GetOnEnterAction())
                {
                    TriggerAction(enterAction);
                }
            }
        }

        private void TriggerExitAction()
        {
            if (currentStoryNode != null)
            {
                foreach (var exitAction in currentStoryNode.GetOnExitAction())
                {
                    TriggerAction(exitAction);
                }
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

        // Events
        public void AdjustStats()
        {
            PlayerPresenter playerPresenter = FindObjectOfType<PlayerPresenter>();
            if (currentStoryNode.GetIsAdjustmentValueRandom())
            {
                int adjustmentValue = FindObjectOfType<DiceRoller>().GenerateDiceRollResult(currentStoryNode.GetRandomDiceToRoll());
                playerPresenter.AdjustStat(currentStoryNode.GetStatsToAdjust(0), adjustmentValue);
            }
            else
            {
                for (int i = 0; i < currentStoryNode.GetStatsCount(); i++)
                {
                    playerPresenter.AdjustStat(currentStoryNode.GetStatsToAdjust(i), currentStoryNode.GetAdjustmentValue(i));
                }
            }
        }

        public void AddItemToInventory()
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            InventoryItem[] items = currentStoryNode.GetItems();
            for (int i = 0; i < items.Length; i++)
            {
                inventory.AddToFirstEmptySlot(items[i]);
            }
        }

        public void RemoveItemFromInventory()
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            InventoryItem[] items = currentStoryNode.GetItems();
            for (int i = 0; i < items.Length; i++)
            {
                int slot = inventory.GetItemSlot(items[i]);
                if (slot >= 0)
                {
                    inventory.RemoveFromSlot(slot);
                }
            }
        }

        public void TestStat()
        {
            Stats[] stats = currentStoryNode.GetStatsToTest();
            if (stats == null || stats.Length > 2) {  return; }
            PlayerPresenter playerPresenter = FindObjectOfType<PlayerPresenter>();
            if (stats.Length == 1)
            {
                hasPassedTest = playerPresenter.TestStat(stats[0], false);
            }
            else
            {
                hasPassedTest = playerPresenter.TestStat(stats[0], stats[1]);
            }
        }

        // Interfaces
        public object CaptureState()
        {
            return currentStoryNode.name;
        }

        public void RestoreState(object state)
        {
            MoveToNextNode((string)state);
            OnStoryUpdate();
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasPassedTest":
                    return hasPassedTest;
            }

            return null;
        }
    }
}