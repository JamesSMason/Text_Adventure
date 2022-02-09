using System.IO;
using UnityEditor;

namespace Adventure.Story.Editor
{
    public class StoryModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationpath)
        {
            StorySO storySO = AssetDatabase.LoadMainAssetAtPath(sourcePath) as StorySO;
            if (storySO == null)
            {
                return AssetMoveResult.DidNotMove;
            }

            if (Path.GetDirectoryName(sourcePath) != Path.GetDirectoryName(destinationpath))
            {
                return AssetMoveResult.DidNotMove;
            }

            storySO.name = Path.GetFileNameWithoutExtension(destinationpath);

            return AssetMoveResult.DidNotMove;
        }
    }
}