using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class GenerateFolders : EditorWindow
{
    private ProjectStructure _folders;

    private static GenerateFolders _window;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Project/Generate/Folders")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        _window = (GenerateFolders)EditorWindow.GetWindow(typeof(GenerateFolders));
        _window.maxSize = new Vector2(300, 100);
        _window.minSize = new Vector2(300, 100);
        _window.Show();
    }



    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        _folders = EditorGUILayout.ObjectField("", _folders, typeof(ScriptableObject), true) as ProjectStructure;
        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Generate Project Folders"))
        {
            if (_folders == null)
            {
                ShowNotification(new GUIContent("No asset provided"));
            }
            else
            {
                ShowNotification(new GUIContent("Generating Folders. This action may take some time"));
                GenerateFolderss(_folders.Assets,0,"Assets");
            }
        }
    }

    private void GenerateFolderss(List<Folder> f, int position, string parentPath)
    {
        if (position == f.Count)
            return;
        Debug.Log($"Folder Generated {parentPath}/{f[position].Name}");
        _ = !AssetDatabase.IsValidFolder($"{parentPath}/{f[position].Name}") ? AssetDatabase.CreateFolder($"{parentPath}", $"{f[position].Name}") : null;
        if (f[position].SubFolders.Count!=0)
        {
            GenerateFolderss(f[position].SubFolders, 0, parentPath+"/"+f[position].Name);
        }
        position++;
        GenerateFolderss(f, position, parentPath);
    }
}

[Serializable]
public class Folder
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public List<Folder> SubFolders;
}


