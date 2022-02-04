using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Adventure.Story.Editor
{
    public class StoryEditor : EditorWindow
    {
        StorySO selectedStory = null;
        GUIStyle nodeStyle = null;

        StoryNode draggingNode = null;
        Vector2 draggingOffset;

        [MenuItem("Window/Text Adventure/Story Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(StoryEditor), false, "Story Editor", true);
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            StorySO storySO = EditorUtility.InstanceIDToObject(instanceID) as StorySO;
            if (storySO != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        void OnGUI()
        {
            if (selectedStory == null)
            {
                EditorGUILayout.LabelField("No story file selected.");
            }
            else
            {
                ProcessEvents();
                foreach (StoryNode node in selectedStory.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (StoryNode node in selectedStory.GetAllNodes())
                {
                    DrawNode(node);
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedStory, "Move Story Node");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
        }

        private void DrawNode(StoryNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Node:", EditorStyles.whiteLabel);
            string newUniqueID = EditorGUILayout.TextField(node.uniqueID);
            string newText = EditorGUILayout.TextField(node.storyText);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedStory, "Update Story Text");
                node.storyText = newText;
                node.uniqueID = newUniqueID;
            }

            GUILayout.EndArea();
        }

        private void DrawConnections(StoryNode node)
        {
            Vector3 startPosition = new Vector2(node.rect.xMax, node.rect.center.y);
            foreach (StoryNode childNode in selectedStory.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.rect.xMin, childNode.rect.center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0.0f;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(startPosition, endPosition, startPosition + controlPointOffset, endPosition - controlPointOffset, Color.white, null, 4f);
            }
        }

        private void OnSelectionChanged()
        {
            StorySO newStory = Selection.activeObject as StorySO;
            if (newStory != null)
            {
                selectedStory = newStory;
            }
            Repaint();
        }

        private StoryNode GetNodeAtPoint(Vector2 point)
        {
            StoryNode foundNode = null;
            foreach (StoryNode node in selectedStory.GetAllNodes())
            {
                if (node.rect.Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}