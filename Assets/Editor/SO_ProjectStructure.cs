using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Project Structure", menuName = "Create Assets/Project Structure Asset", order = 1)]
public class ProjectStructure : ScriptableObject
{
    public List<Folder> Assets;
}