using System;
using UnityEngine;

namespace Adventure.Story
{
    [Serializable]
    public class StoryNode
    {
        public string uniqueID;
        public string storyText;
        public string[] children;
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}