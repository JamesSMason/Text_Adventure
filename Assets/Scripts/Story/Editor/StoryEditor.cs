using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Adventure.Story.Editor
{
    public class StoryEditor : EditorWindow
    {
        StorySO selectedStory = null;
        [NonSerialized] GUIStyle nodeStyle = null;
        [NonSerialized] StoryNode draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] StoryNode creatingNode = null;
        [NonSerialized] StoryNode deletingNode = null;
        [NonSerialized] StoryNode linkingParentNode = null;
        Vector2 scrollPosition;
        [NonSerialized] bool draggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;

        const float canvasSize = 4000;
        const float backgroundSize = 50;

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

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);
                
                foreach (StoryNode node in selectedStory.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (StoryNode node in selectedStory.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    Undo.RecordObject(selectedStory, "Added Story Node");
                    selectedStory.CreateNode(creatingNode);
                    creatingNode = null;
                }

                if (deletingNode != null)
                {
                    Undo.RecordObject(selectedStory, "Deleted Story Node");
                    selectedStory.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedStory, "Move Story Node");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }
        }

        private void DrawNode(StoryNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();

            string newText = EditorGUILayout.TextField(node.storyText);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedStory, "Update Story Text");
                node.storyText = newText;
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            DrawLinkButtons(node);

            if (GUILayout.Button("x"))
            {
                deletingNode = node;
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButtons(StoryNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("Link"))
                {
                    linkingParentNode = node;
                }
            }
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("Cancel"))
                {
                    linkingParentNode = null;
                }
            }
            else if (linkingParentNode.children.Contains(node.uniqueID))
            {
                if (GUILayout.Button("Unlink"))
                {
                    Undo.RecordObject(selectedStory, "Remove Story Link");
                    linkingParentNode.children.Remove(node.uniqueID);
                    linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("Child"))
                {
                    Undo.RecordObject(selectedStory, "Add Story Link");
                    linkingParentNode.children.Add(node.uniqueID);
                    linkingParentNode = null;
                }
            }
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