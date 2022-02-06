using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Adventure.Story
{
    public class StoryNode : ScriptableObject
    {
        [SerializeField] string storyText;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] List<string> optionText = new List<string>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);

        Dictionary<string, string> childrenLookup = new Dictionary<string, string>();

        public string GetStoryText()
        {
            return storyText;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public Rect GetRect()
        {
            return rect;
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
            children.Add(childID);
            optionText.Add("New option");
            childrenLookup.Add(childID, "New option");
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Story Link");
            children.Remove(childID);
            optionText.Remove(childrenLookup[childID]);
            childrenLookup.Remove(childID);
            EditorUtility.SetDirty(this);
        }
#endif
    }
}