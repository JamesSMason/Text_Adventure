using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Adventure.Story
{
    [CreateAssetMenu(fileName = "New Story", menuName = "Adventure/New Story", order = 0)]
    public class StorySO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] List<StoryNode> nodes = new List<StoryNode>();

        Dictionary<string, StoryNode> nodeLookup = new Dictionary<string, StoryNode>();

        void Awake()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                CreateNode(null);
            }
#endif
            OnValidate();
        }

        void OnValidate()
        {
            nodeLookup.Clear();
            foreach (StoryNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
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

        public void CreateNode(StoryNode parent)
        {
            StoryNode newNode = CreateInstance<StoryNode>();
            newNode.name = Guid.NewGuid().ToString();
            Undo.RegisterCreatedObjectUndo(newNode, "Created Story Node");
            if (parent != null)
            {
                parent.children.Add(newNode.name);
            }
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(StoryNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void CleanDanglingChildren(StoryNode nodeToDelete)
        {
            foreach (StoryNode node in GetAllNodes())
            {
                node.children.Remove(nodeToDelete.name);
            }
        }

        public void OnBeforeSerialize()
        {
            if (nodes.Count == 0)
            {
                CreateNode(null);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (StoryNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
        }

        public void OnAfterDeserialize()
        {

        }
    }
}