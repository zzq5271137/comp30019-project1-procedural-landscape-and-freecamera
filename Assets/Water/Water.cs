using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Water : MonoBehaviour
{
    public sunScript sun;
    public moonScript moon;
    public int allDevisions = 128;
    public float meshSize = 10;

    private Vector3[] verts;
    private int vertscount;

    // Start is called before the first frame update
    void Start()
    {
        CreateWater();
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer render = this.GetComponent<MeshRenderer>();

        render.material.SetColor("_sunColor", sun.color);
        render.material.SetVector("_sunPosition", sun.getPosition());
    }

    // Create water plane
    public Mesh CreateWater()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertscount = (allDevisions + 1) * (allDevisions + 1);
        verts = new Vector3[vertscount];
        Vector2[] uvs = new Vector2[vertscount];
        int[] tris = new int[allDevisions * allDevisions * 6];

        float halfsize = meshSize * 0.5f;
        float divsize = meshSize / allDevisions;

        int trioffset = 0;

        for (int i = 0; i <= allDevisions; i++)
        {
            for (int j = 0; j <= allDevisions; j++)
            {
                verts[i * (allDevisions + 1) + j] = new Vector3(
                    -halfsize + j * divsize, 0.0f, halfsize - i * divsize);
                uvs[i * (allDevisions + 1) + j] =
                    new Vector2((float) i / allDevisions,
                        (float) j / allDevisions);

                if ((i < allDevisions) && (j < allDevisions))
                {
                    int topleft = i * (allDevisions + 1) + j;
                    int bottomleft = (i + 1) * (allDevisions + 1) + j;

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

        mesh.vertices = verts;

        Color[] colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            colors[i] = Color.blue;
        }

        mesh.colors = colors;

        mesh.triangles = tris;

        // normals are clearly all simply Vector3.up
        Vector3[] normals = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            normals[i] = Vector3.up;
        }

        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}