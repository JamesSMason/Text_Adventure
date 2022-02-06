using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Adventure.Story
{
    public class StoryNode : ScriptableObject
    {
        [SerializeField] string storyText;
        [SerializeField] List<ChildNode> children = new List<ChildNode>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);

        const string newOptionText = "New option";

        public string GetStoryText()
        {
            return storyText;
        }

        public List<string> GetChildren()
        {
            List<string> childIDs = new List<string>();
            foreach (ChildNode child in children)
            {
                childIDs.Add(child.GetChildID());
            }
            return childIDs;
        }

        public Rect GetRect()
        {
            return rect;
        }

        public string GetOption(string childID)
        {
            foreach (ChildNode child in children)
            {
                if (child.GetChildID() == childID)
                { 
                    return child.GetOptionText();
                }
            }
            return null;
        }

#if UNITY_EDITOR
        public void SetStoryText(string newText)
        {
            if (newText != storyText)
            {
                Undo.RecordObject(this, "Update Story Text");
                storyText = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Story Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Story Link");
            ChildNode newNode = new ChildNode(childID, newOptionText);
            children.Add(newNode);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Story Link");
            foreach (ChildNode childNode in children)
            {
                if (childNode.GetChildID() == childID)
                {
                    children.Remove(childNode);
                    break;
                }
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
}