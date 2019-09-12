using UnityEngine;

/*
 * Reference from https://www.youtube.com/watch?v=1HV8GbFnCik
 */

public class DiamondSquareAlgorithm
{
    public struct ResultSet
    {
        public Vector3[] allVertices;
        public Vector2[] uvs;
        public int[] allTriangles;
    }

    private static Vector3[] allVertices;
    private static int verticesCount;

    public static ResultSet CreateTerrain(int allDivisions, float meshSize,
        float meshHeight)
    {
        verticesCount = (allDivisions + 1) * (allDivisions + 1);
        allVertices = new Vector3[verticesCount];
        Vector2[] uvs = new Vector2[verticesCount];
        int[] tris = new int[allDivisions * allDivisions * 6];

        float halfSize = meshSize * 0.5f;
        float divisionSize = meshSize / allDivisions;

        int trioffset = 0;

        for (int i = 0; i <= allDivisions; i++)
        {
            for (int j = 0; j <= allDivisions; j++)
            {
                allVertices[i * (allDivisions + 1) + j] = new Vector3(
                    -halfSize + j * divisionSize, 0.0f,
                    halfSize - i * divisionSize);
                uvs[i * (allDivisions + 1) + j] =
                    new Vector2((float) i / allDivisions,
                        (float) j / allDivisions);
                if (i < allDivisions && j < allDivisions)
                {
                    int topleft = i * (allDivisions + 1) + j;
                    int bottomleft = (i + 1) * (allDivisions + 1) + j;

                    tris[trioffset] = topleft;
                    tris[trioffset + 1] = topleft + 1;
                    tris[trioffset + 2] = bottomleft + 1;

                    tris[trioffset + 3] = topleft;
                    tris[trioffset + 4] = bottomleft + 1;
                    tris[trioffset + 5] = bottomleft;

                    trioffset += 6;
                }
            }
        }

        allVertices[0].y = Random.Range(-meshHeight, meshHeight);
        allVertices[allDivisions].y = Random.Range(-meshHeight, meshHeight);
        allVertices[allVertices.Length - 1].y =
            Random.Range(-meshHeight, meshHeight);
        allVertices[allVertices.Length - 1 - allDivisions].y =
            Random.Range(-meshHeight, meshHeight);

        int iterations = (int) Mathf.Log(allDivisions, 2);
        int numSquares = 1;
        int squareSize = allDivisions;

        for (int i = 0; i < iterations; i++)
        {
            int row = 0;
            for (int j = 0; j < numSquares; j++)
            {
                int col = 0;
                for (int k = 0; k < numSquares; k++)
                {
                    DiamondSquare(row, col, squareSize, meshHeight,
                        allDivisions);
                    col += squareSize;
                }

                row += squareSize;
            }

            numSquares *= 2;
            squareSize /= 2;
            meshHeight *= 0.5f;
        }

        ResultSet resultSet = new ResultSet()
        {
            allVertices = allVertices,
            uvs = uvs,
            allTriangles = tris
        };
        return resultSet;
    }

    private static  void DiamondSquare(int row, int column, int size, float offset,
        int allDivisions)
    {
        int halfsize = (int) (size * 0.5f);
        int topleft = row * (allDivisions + 1) + column;
        int bottomleft = (row + size) * (allDivisions + 1) + column;

        int mid = (int) (row + halfsize) * (allDivisions + 1) +
                  (int) (column + halfsize);
        allVertices[mid].y = (allVertices[topleft].y +
                              allVertices[topleft + size].y +
                              allVertices[bottomleft].y +
                              allVertices[bottomleft + size].y) *
                             0.25f + Random.Range(-offset, offset);

        allVertices[topleft + halfsize].y =
            (allVertices[topleft].y + allVertices[topleft + size].y +
             allVertices[mid].y) / 3 +
            Random.Range(-offset, offset);

        allVertices[mid - halfsize].y =
            (allVertices[topleft].y + allVertices[bottomleft].y +
             allVertices[mid].y) / 3 +
            Random.Range(-offset, offset);

        allVertices[mid + halfsize].y =
            (allVertices[topleft + size].y + allVertices[bottomleft].y +
             allVertices[mid].y) /
            3 + Random.Range(-offset, offset);

        allVertices[bottomleft + halfsize].y =
            (allVertices[bottomleft].y + allVertices[bottomleft + size].y +
             allVertices[mid].y) / 3 + Random.Range(-offset, offset);
    }
}