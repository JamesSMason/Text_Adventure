using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    [Serializable]
    public class StoryNode
    {
        public string uniqueID;
        public string storyText;
        public List<string> children = new List<string>();
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}