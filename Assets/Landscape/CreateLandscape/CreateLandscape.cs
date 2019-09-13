using UnityEngine;

/**
 * The reason to create a separate class to call the function in
 * ProceduralTerrain.cs to create the terrain is that, we use an editor
 * to create a button for conveniently create landscape as we want.
 * But the editor will overlap with the default Terrain editor which will disable
 * the default editor. So we need this separate class.
 */

public class CreateLandscape : MonoBehaviour
{
    // the number of all divisions, default 128 * 128
    public int allDivisions = 128;
    public float meshSize = 10;
    public float meshHeight = 50;
    public float scale = 15f;

    public void CreateTerrain()
    {
        ProceduralTerrain proceduralTerrain =
            FindObjectOfType<ProceduralTerrain>();
        proceduralTerrain.CreateTerrain(allDivisions, meshSize, meshHeight,
            scale);
    }
}