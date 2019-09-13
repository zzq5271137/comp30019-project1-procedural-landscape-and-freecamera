using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    // on which the procedural will be generated
    public Terrain terrain;

    public void CreateTerrain(int allDivisions, float meshSize,
        float meshHeight, float scale)
    {
        /*
         * Get corresponding terrain data and set the heights calculated by
         * our diamond square algorithm.
         */
        TerrainData terrainData = terrain.terrainData;
        DiamondSquareAlgorithm.ResultSet resultSet =
            DiamondSquareAlgorithm.CreateTerrain(allDivisions, meshSize,
                meshHeight);
        terrainData.SetHeights(0, 0,
            GetHeights(terrainData, resultSet, scale));
    }

    private float[,] GetHeights(TerrainData terrainData,
        DiamondSquareAlgorithm.ResultSet resultSet, float scale)
    {
        // the height here refers to the length of the terrain
        int width = terrainData.heightmapWidth;
        int height = terrainData.heightmapHeight;
        float[,] heights = new float[width, height];

        float lowestPoint = resultSet.allVertices[0].y;
        for (int i = 0; i < resultSet.allVertices.Length; i++)
        {
            if (resultSet.allVertices[i].y < lowestPoint)
            {
                lowestPoint = resultSet.allVertices[i].y;
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