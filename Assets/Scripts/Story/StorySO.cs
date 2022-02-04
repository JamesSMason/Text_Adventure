using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    [CreateAssetMenu(fileName = "New Story", menuName = "Adventure/New Story", order = 0)]
    public class StorySO : ScriptableObject
    {
        [SerializeField] List<StoryNode> nodes = new List<StoryNode>();

#if UNITY_EDITOR
        void Awake()
        {
            if (nodes.Count == 0)
            {
                nodes.Add(new StoryNode());
            }
        }
#endif

        public IEnumerable<StoryNode> GetAllNodes()
        {
            return nodes;
        }
    }
}