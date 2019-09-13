using UnityEngine;

public class RanderLandscape : MonoBehaviour
{
    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex;
        public int straightHeight;
    }

    public SplatHeights[] splatHeights;

    public void randerLandscape()
    {
        TerrainData terrainData = GetComponent<Terrain>().terrainData;
        float[,,] splatmapData = new float[terrainData.alphamapWidth,
            terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float height = terrainData.GetHeight(y, x);
                float[] splat = new float[splatHeights.Length];

                for (int i = 0; i < splatHeights.Length; i++)
                {
                    if (height >= splatHeights[i].straightHeight)
                    {
                        if (i == splatHeights.Length - 1)
                        {
                            splat[i] = 1;
                        }
                        else
                        {
                            if (height < splatHeights[i + 1].straightHeight)
                            {
                                splat[i] = 1;
                            }
                        }
                    }
                }

                for (int i = 0; i < splatHeights.Length; i++)
                {
                    splatmapData[x, y, i] = splat[i];
                }
            }
        }

        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}