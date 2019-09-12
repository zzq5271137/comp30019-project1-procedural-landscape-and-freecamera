using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralTerrain))]
public class TerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ProceduralTerrain createLandscape = (ProceduralTerrain) target;

        DrawDefaultInspector();

        if (GUILayout.Button("Create Terrain"))
        {
            createLandscape.CreateTerrain();
        }
    }
}