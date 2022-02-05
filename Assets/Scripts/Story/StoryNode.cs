using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    public class StoryNode : ScriptableObject
    {
        public string storyText;
        public List<string> children = new List<string>();
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}