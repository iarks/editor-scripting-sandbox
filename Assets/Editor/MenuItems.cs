using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class MenuItems
{
    [MenuItem("Project" + "/" + "Create" + "/" + "Folders" + "/" + "Small Project")]
    static void CreateFolderStructure()
    {
        _ = !AssetDatabase.IsValidFolder("Assets/Scripts") ? AssetDatabase.CreateFolder("Assets", "Scripts") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/Scenes") ? AssetDatabase.CreateFolder("Assets", "Scenes") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/Resources") ? AssetDatabase.CreateFolder("Assets", "Resources") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/Shaders") ? AssetDatabase.CreateFolder("Assets", "Shaders") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/Materials") ? AssetDatabase.CreateFolder("Assets", "Materials") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/Prefabs") ? AssetDatabase.CreateFolder("Assets", "Prefabs") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/StreamingAssets") ? AssetDatabase.CreateFolder("Assets", "StreamingAssets") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/Plugins") ? AssetDatabase.CreateFolder("Assets", "Plugins") : null;

        _ = !AssetDatabase.IsValidFolder("Assets/StaticAssets/Videos") ? AssetDatabase.CreateFolder("Assets/Resources", "Videos") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/StaticAssets/Fonts") ? AssetDatabase.CreateFolder("Assets/Resources", "Fonts") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/StaticAssets/Images") ? AssetDatabase.CreateFolder("Assets/Resources", "Images") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/StaticAssets/Animations") ? AssetDatabase.CreateFolder("Assets/Resources", "Animations") : null;

        _ = !AssetDatabase.IsValidFolder("Assets/StaticAssets/Images/Sprites") ? AssetDatabase.CreateFolder("Assets/Resources/Images", "Sprites") : null;
        _ = !AssetDatabase.IsValidFolder("Assets/StaticAssets/Images/Textures") ? AssetDatabase.CreateFolder("Assets/Resources/Images", "Textures") : null;
    }

    [MenuItem("Project" + "/" + "Clean")]
    static void CleanFolders()
    {
        string[] allPaths = AssetDatabase.GetAllAssetPaths().Where(p=> p.Contains("Assets/") && !p.Contains("Assets/Editor") && !Regex.Match(p, "Assets/.*/").Success).ToArray();
        for(var i = 0;i<allPaths.Length;i++)
        {
            _ = FileUtil.DeleteFileOrDirectory($"{ allPaths[i]}.meta") ? _ = FileUtil.DeleteFileOrDirectory($"{ allPaths[i]}") ? true : false: false;
        }
        AssetDatabase.Refresh();
    }
}
