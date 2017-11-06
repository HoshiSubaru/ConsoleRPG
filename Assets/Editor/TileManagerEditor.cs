using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileManager))]
public class TileManagerEditor : Editor
{
    public TileManager myTarget;

    public override void OnInspectorGUI()
    {
        myTarget = (TileManager)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
        
    }

    public void GenerateGrid()
    {
        myTarget.GenerateGrid();
    }
}