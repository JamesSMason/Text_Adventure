using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Story
{
    [CreateAssetMenu(fileName = "New Story", menuName = "Adventure/New Story", order = 0)]
    public class StorySO : ScriptableObject
    {
        [SerializeField] List<StoryNode> nodes = new List<StoryNode>();

        Dictionary<string, StoryNode> nodeLookup = new Dictionary<string, StoryNode>();

        void Awake()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                nodes.Add(new StoryNode());
            }
#endif
            OnValidate();
        }

        void OnValidate()
        {
            nodeLookup.Clear();
            foreach (StoryNode node in GetAllNodes())
            {
                nodeLookup[node.uniqueID] = node;
            }
        }

        public IEnumerable<StoryNode> GetAllNodes()
        {
            return nodes;
        }

        public StoryNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<StoryNode> GetAllChildren(StoryNode parentNode)
        {
            foreach (string childID in parentNode.children)
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }
    }
}