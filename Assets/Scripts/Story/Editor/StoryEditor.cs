using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Adventure.Story.Editor
{
    public class StoryEditor : EditorWindow
    {
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
    }
}