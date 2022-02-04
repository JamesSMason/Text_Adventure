using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Adventure.Story.Editor
{
    public class StoryEditor : EditorWindow
    {
        StorySO selectedStory = null;

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
        }

        void OnGUI()
        {
            if (selectedStory == null)
            {
                EditorGUILayout.LabelField("No story file selected.");
            }
            else
            {
                foreach (StoryNode node in selectedStory.GetAllNodes())
                {
                    EditorGUILayout.LabelField($"{node.storyText}");
                }
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
    }
}