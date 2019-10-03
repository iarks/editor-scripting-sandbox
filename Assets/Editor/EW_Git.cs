using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class Git : EditorWindow
{
    private static Git _window;

    private string _consoleOutput;

    private string _commitMessage;

    private string _commitSubject;

    private string _commitFooter;

    private string[] _porcelainModified = { "M " };
    private string[] _porcelainUntracked = { "?? " };

    int index;

    string[] options = { "feat", "refactor", "fix", "docs", "style", "test", "chore", "revert" };

    string[] str = { "git status\r\n" };

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Git")]
    static void Init()
    {
        
        // Get existing open window or if none, make a new one:
        _window = (Git)EditorWindow.GetWindow(typeof(Git));
        _window.maxSize = new Vector2(300, 100);
        _window.minSize = new Vector2(500, 500);
        _window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("git status"))
        {
            RunCmd("git status");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("git add ."))
        {
            RunCmd("git add .");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("clear"))
        {
            RunCmd("cls");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        index = EditorGUILayout.Popup(index, options);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Subject");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextArea(_commitSubject);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Body");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextArea(_commitMessage, new GUILayoutOption[] { GUILayout.MinHeight(70)});
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Footer");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextArea(_commitFooter, new GUILayoutOption[] { GUILayout.MinHeight(50) });
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Commit"))
        {
            RunCmd("git --version");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Changes"))
        {
            var modifiedFiles = new List<string>();
            var untrackedFiles = new List<string>();
            var changes = RunCmd("git status --porcelain");
            var xx = changes.Split(new string[] { "\n" }, StringSplitOptions.None);
            for(int i=4; i<xx.Length; i++)
            {
                if(xx[i].StartsWith(" M ") || xx[i].StartsWith("M "))
                {
                    modifiedFiles.Add(xx[i]);
                }
                else if (xx[i].StartsWith(" ?? ") || xx[i].StartsWith("?? "))
                {
                    untrackedFiles.Add(xx[i]);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(_consoleOutput, new GUILayoutOption[] { GUILayout.MinHeight(200), GUILayout.MinWidth(500)});
        EditorGUILayout.EndHorizontal();
    }

    private void AddComponentToObjects()
    {
        throw new NotImplementedException();
    }

    private string RunCmd(string v)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        cmd.StandardInput.WriteLine(v);
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        var x = cmd.StandardOutput.ReadToEnd();
        cmd.WaitForExit();
        return x;
    }
}
