using System;

namespace Adventure.Story
{
    [Serializable]
    public class StoryNode
    {
        public string uniqueID;
        public string storyText;
        public string[] children;
    }
}