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
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

        public StoryNode GetStoryNode(string childID)
        {
            if (nodeLookup.ContainsKey(childID))
            {
                return nodeLookup[childID];
            }
            return null;
        }

#if UNITY_EDITOR
        public void CreateNode(StoryNode parent)
        {
            StoryNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Story Node");
            Undo.RecordObject(this, "Added Story Node");
            AddNode(newNode);
        }

        private void AddNode(StoryNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(StoryNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Story Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private StoryNode MakeNode(StoryNode parent)
        {
            StoryNode newNode = CreateInstance<StoryNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
            }

            return newNode;
        }

        private void CleanDanglingChildren(StoryNode nodeToDelete)
        {
            foreach (StoryNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                StoryNode newNode = MakeNode(null);
                AddNode(newNode);
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
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}