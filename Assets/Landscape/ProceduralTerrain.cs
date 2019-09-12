using UnityEngine;

/*
 * Reference from https://www.youtube.com/watch?v=vFvwyu_ZKfU&t=465s
 */

public class ProceduralTerrain : MonoBehaviour
{
    // on which the procedural will be generated
    public Terrain terrain;

    // the number of all divisions, default 128 * 128
    public int allDivisions = 128;

    public float meshSize = 30;
    public float meshHeight = 50;
    public float scale = 15f;

    public void CreateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        DiamondSquareAlgorithm.ResultSet resultSet =
            DiamondSquareAlgorithm.CreateTerrain(allDivisions, meshSize,
                meshHeight);
        terrainData.SetHeights(0, 0,
            GetHeights(terrainData, resultSet));
    }

    private float[,] GetHeights(TerrainData terrainData,
        DiamondSquareAlgorithm.ResultSet resultSet)
    {
        // the height here refers to the length of the terrain
        int width = terrainData.heightmapWidth;
        int height = terrainData.heightmapHeight;
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        float[] verticesHeight = new float[resultSet.allVertices.Length];
        for (int i = 0; i < resultSet.allVertices.Length; i++)
        {
            verticesHeight[i] = resultSet.allVertices[i].y;
        }

        float lowestPoint = verticesHeight[0];
        for (int i = 0; i < verticesHeight.Length; i++)
        {
            if (verticesHeight[i] < lowestPoint)
            {
                lowestPoint = verticesHeight[i];
            }
        }

        lowestPoint = Mathf.Abs(lowestPoint);

        int k = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 3000 here refers to the terrain height in terrain resolution
                // (the distance above the horizon line)
                heights[x, y] = (lowestPoint + resultSet.allVertices[k].y) *
                                scale / 3000;
                k++;
            }
        }

        return heights;
    }
}