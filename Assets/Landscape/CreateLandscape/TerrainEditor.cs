using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CreateLandscape))]
public class TerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CreateLandscape createLandscape = (CreateLandscape) target;
        DrawDefaultInspector();
        if (GUILayout.Button("Create Terrain"))
        {
            createLandscape.CreateTerrain();
        }
    }
}