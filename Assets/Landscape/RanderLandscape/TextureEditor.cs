using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RanderLandscape))]
public class TextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RanderLandscape randerLandscape = (RanderLandscape) target;
        DrawDefaultInspector();
        if (GUILayout.Button("Rander Terrain"))
        {
            randerLandscape.randerLandscape();
        }
    }
}
